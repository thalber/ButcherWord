using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;
using MonoMod;
using System.IO;
using System.Reflection;

namespace WaspPile.ButchersWord
{
    [Obsolete]
    public static class PassiveButcher
    {
        static PassiveButcher()
        {
            //Assembly.LoadFile(@"C:\Program Files (x86)\Steam\steamapps\common\Rain World\BepInEx\patchers\Newtonsoft.Json.dll");
        }
        public static IEnumerable<string> TargetDLLs => new[] { "Assembly-CSharp.dll" };
        public static void Patch(AssemblyDefinition assembly)
        {
            foreach (TypeDefinition td in assembly.MainModule.Types)
            {

            }
        }
        [Obsolete]
        public static string GetDescriptionForType(TypeDefinition td)
        {
            string RESULT = td.FullName + " : ";
            RESULT += td.BaseType.Name;
            if (td.HasInterfaces)
            {
                foreach (InterfaceImplementation inf in td.Interfaces)
                {
                    RESULT += ", ";
                    RESULT += inf.InterfaceType.Name;
                }
            }
            RESULT += $"\n===GENERAL INFO===\n<INSERT {td.Name} CLASS DESCRIPTION>\n\n";
            RESULT += "===METHODS===\n";
            foreach (MethodDefinition method in td.Methods)
            {
                RESULT += $"=={method.Name}==\n";
                RESULT += ((method.IsStatic)? "static " : "") + method.ReturnType.Name + ' ' + method.Name + '(';
                bool HasParameters = false;
                foreach (ParameterDefinition pd in method.Parameters)
                {
                    HasParameters = true;
                    RESULT += pd.ParameterType.Name;
                    RESULT += ' ';
                    RESULT += pd.Name;
                    RESULT += ", ";
                }
                if (HasParameters) RESULT = RESULT.Remove(RESULT.Length - 2, 2);
                RESULT += ")\n";
                RESULT += "<INSERT METHOD DESCRIPTION HERE>\n\n";
            }
            RESULT += "\n===PROPERTIES===\n";
            foreach (PropertyDefinition property in td.Properties)
            {
                RESULT += $"=={property.Name}==\n";
                RESULT += property.PropertyType.Name + ' ' + property.Name + " {" + ((property.GetMethod != null) ? "get; " : "") + ((property.SetMethod != null) ? "set; " : "") + "}\n";
                RESULT += "<INSERT PROPERTY DESCRIPTION HERE>\n\n";
            }
            RESULT += "\n===FIELDS===\n";
            foreach (FieldDefinition field in td.Fields)
            {
                RESULT += $"=={field.Name}==\n";
                RESULT += ((field.IsStatic)? "static " : "") + field.FieldType.Name + ' ' + field.Name + '\n';
                RESULT += "INSERT FIELD DESCRIPTION HERE\n\n";
            }
            return RESULT;
        }
    }
}
