using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;

namespace WaspPile.ButchersWord
{
    public class TypeVein
    {
        public TypeVein (TypeDefinition tartype)
        {
            Name = tartype.FullName;
            IsInterface = tartype.IsInterface;
            Namespace = tartype.Namespace;
            IsSealed = tartype.IsSealed;
            if (tartype.BaseType != null) BaseClassName = tartype.BaseType.Name;
            Methods = new MethodVein[tartype.Methods.Count];
            Properties = new PropertyVein[tartype.Properties.Count];
            Fields = new FieldVein[tartype.Fields.Count];
            NestedTypes = new TypeVein[tartype.NestedTypes.Count];
            Interfaces = new InterfaceVein[tartype.Interfaces.Count];
            for (int i = 0; i < Methods.Length; i++) Methods[i] = new MethodVein(tartype.Methods[i]);
            for (int i = 0; i < Properties.Length; i++) Properties[i] = new PropertyVein(tartype.Properties[i]);
            for (int i = 0; i < Fields.Length; i++) Fields[i] = new FieldVein(tartype.Fields[i]);
            for (int i = 0; i < NestedTypes.Length; i++) NestedTypes[i] = new TypeVein(tartype.NestedTypes[i]);
            for (int i = 0; i < Interfaces.Length; i++) Interfaces[i] = new InterfaceVein(tartype.Interfaces[i]);
        }
        public string Namespace;
        public string Name;
        public string BaseClassName;
        public bool IsSealed;
        public bool IsInterface;
        public MethodVein[] Methods;
        public PropertyVein[] Properties;
        public FieldVein[] Fields;
        public TypeVein[] NestedTypes;
        public InterfaceVein[] Interfaces;
    }

    public class MethodVein
    {
        public MethodVein (MethodDefinition method)
        {
            IsStatic = method.IsStatic;
            Name = method.FullName;
            Parameters = new ParameterVein[method.Parameters.Count];
            ReturnType = method.ReturnType.FullName;
            if (Parameters.Length > 0)
            {
                for (int i = 0; i < method.Parameters.Count; i++) Parameters[i] = new ParameterVein(method.Parameters[i]);
            }
        }
        public string Name;
        public bool IsStatic;
        public string ReturnType;
        public ParameterVein[] Parameters;
        
        public class ParameterVein
        {
            public ParameterVein(ParameterDefinition parameter)
            {
                Name = parameter.Name;
                ParameterType = parameter.ParameterType.FullName;
            }
            public string ParameterType;
            public string Name;
        }
    }

    public class PropertyVein
    {
        public PropertyVein(PropertyDefinition property)
        {
            HasGetter = property.GetMethod != null;
            HasSetter = property.SetMethod != null;
            if (property.GetMethod != null && property.GetMethod.IsStatic) IsStatic = true;
            if (property.SetMethod != null && property.SetMethod.IsStatic) IsStatic = true;
            Name = property.FullName;
            PropertyType = property.PropertyType.FullName;
            
            //if (property.PropertyType.HasGenericParameters)
            //{
            //    GenericParameters = new string[property.PropertyType.GenericParameters.Count];
            //    for (int i = 0; i < GenericParameters.Length; i++)
            //    {
            //        GenericParameters[i] = property.PropertyType.GenericParameters[i].Name;
            //    }
            //}
        }
        public string Name;
        public string PropertyType;
        public bool HasGetter;
        public bool HasSetter;
        public bool IsStatic;
        //public string[] GenericParameters;
    }

    public class InterfaceVein
    {
        public InterfaceVein(InterfaceImplementation inf)
        {
            Name = inf.InterfaceType.FullName;
        }
        public string Name;

    }

    public class FieldVein
    {
        public FieldVein(FieldDefinition field)
        {
            IsStatic = field.IsStatic;
            FieldType = field.FieldType.FullName;
            Name = field.FullName;
            //if (field.)
            //{
            //    GenericParameters = new string[field.FieldType.GenericParameters.Count];
            //    for (int i = 0; i < GenericParameters.Length; i++) GenericParameters[i] = field.FieldType.GenericParameters[i].Name;
            //}
        }
        public string Name;
        public bool IsStatic;
        public string FieldType;
        //public string[] GenericParameters;
    }
}
