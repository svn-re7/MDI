using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using PluginInterface;

namespace MDI
{
    public static class PluginCompiler
    {
        public static IPlugin? Compile(string sourceCode, out string errors)
        {
            errors = string.Empty;

            // дерево синтаксического анализа для кода
            var syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
            var assemblyName = Path.GetRandomFileName();

            // ссылки на все загруженные сборки приложения
            var references = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic && !string.IsNullOrWhiteSpace(a.Location))
                .Select(a => MetadataReference.CreateFromFile(a.Location))
                .Cast<MetadataReference>()
                .ToList();

            // дополнительные базовые зависимости
            var basicTypes = new[] { typeof(object), typeof(Enumerable), typeof(Bitmap), typeof(IPlugin) };
            foreach (var type in basicTypes)
            {
                var loc = type.Assembly.Location;
                if (!references.Any(r => r is PortableExecutableReference per && per.FilePath == loc))
                {
                    references.Add(MetadataReference.CreateFromFile(loc));
                }
            }

            // объект сборки для компиляции
            var compilation = CSharpCompilation.Create(
                assemblyName,
                new[] { syntaxTree },
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            // компиляция в поток памяти
            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                    // сбор ошибок компиляции
                    var failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (var diagnostic in failures)
                    {
                        errors += $"{diagnostic.Id}: {diagnostic.GetMessage()}\n";
                    }

                    return null;
                }

                // загрузка сборки из памяти
                ms.Seek(0, SeekOrigin.Begin);
                var assembly = Assembly.Load(ms.ToArray());
                
                // поиск класса с интерфейсом IPlugin
                var pluginType = assembly.GetTypes()
                    .FirstOrDefault(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                if (pluginType == null)
                {
                    errors = "класс с интерфейсом IPlugin не найден";
                    return null;
                }

                // создание экземпляра плагина
                return (IPlugin?)Activator.CreateInstance(pluginType);
            }
        }
    }
}
