${
    // Enable extension methods by adding using Typewriter.Extensions.*
    using Typewriter.Extensions.Types;
    using System.Text.RegularExpressions;
    using System.Diagnostics;

    Template(Settings settings)
    {
        settings.IncludeProject("Reroll.Models");
    }

    // Custom extension methods can be used in the template by adding a $ prefix e.g. $LoudName
    string LoudName(Property property)
    {
        return property.Name.ToUpperInvariant();
    }

    string ToKebabCase(string typeName){
        return  Regex.Replace(typeName, "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])","-$1", RegexOptions.Compiled)
                     .Trim().ToLower();
    }

    string CleanupName(string propertyName, bool? removeArray = true){
        if (removeArray.HasValue && removeArray.Value) {
            propertyName = propertyName.Replace("[]","");
        }
        return propertyName.Replace("Model","");
    }

    string Imports(Class c) => c.Properties
                                .Where(p=>p.Type.Name != "ObjectId")
                                .Where(p=>!p.Type.IsPrimitive || p.Type.IsEnum)
                                .Select(p=> $"import {{ {CleanupName(p.Type.Name)} }} from './{(CleanupName(p.Type.Name))}';")
                                .Aggregate("", (all,import) => $"{all}{import}\r\n")
                                .TrimStart();

    string CustomProperties(Class c) => c.Properties
                                        .Where(p=>p.Name != "Id")
                                        .Select(p=> $"\tpublic {p.name}: {CleanupName(p.Type.Name, false)};")
                                        .Aggregate("", (all,prop) => $"{all}{prop}\r\n")
                                        .TrimEnd();

    string ClassName(Class c) => c.Name.Replace("Model","");

}
     $Classes(Reroll.Models.*)[$Imports
export class $ClassName   {
$CustomProperties
}]$Enums(*)[export enum $Name { $Values[
    $name = $Value][,]
}]
