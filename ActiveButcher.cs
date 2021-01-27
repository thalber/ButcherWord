using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;
using Newtonsoft.Json;

namespace WaspPile.ButchersWord
{
    class ActiveButcher
    {
        class config
        {
            string confpath => System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("ButchersWord.exe", "bw.json");
            public string tarfile;
            public string tarclass;
            public string output;
            public void Load()
            {
                try
                {
                    string json = System.IO.File.ReadAllText(confpath);
                    config newconf = JsonConvert.DeserializeObject<config>(json);
                    tarclass = newconf.tarclass;
                    tarfile = newconf.tarfile;
                    output = newconf.output;

                }
                catch (System.IO.IOException)
                {

                }
            }
            public void Save()
            {
                string s = JsonConvert.SerializeObject(this, Formatting.Indented);
                System.IO.File.WriteAllText(confpath, s);
            }
        }
        public static void Main()
        {
            config cfg = new config();
            cfg.Load();
            Console.WriteLine($"enter target file location (leave empty to export {cfg.tarfile})");
            string targetfile = Console.ReadLine();
            if (targetfile != string.Empty) cfg.tarfile = targetfile;
            ModuleDefinition md = ModuleDefinition.ReadModule(cfg.tarfile);
            Console.WriteLine($"enter target class name (leave empty to export {cfg.tarclass})");
            string targetclass = Console.ReadLine();
            if (targetclass != string.Empty) cfg.tarclass = targetclass;
            foreach (TypeDefinition td in md.GetTypes())
            {
                if (td.Name == cfg.tarclass)
                {
                    
                    TypeVein tv = new TypeVein(td);
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.Formatting = Formatting.Indented;
                    string res = JsonConvert.SerializeObject(tv, settings);
                    Console.WriteLine(res);
                    Console.WriteLine();
                    Console.WriteLine($"enter the path to output json to (leave empty to export to {cfg.output})");
                    string output = Console.ReadLine();
                    if (output != string.Empty) cfg.output = output;
                    if (cfg.output != string.Empty) System.IO.File.WriteAllText(cfg.output, res);
                    goto breakpoint;
                }
            }
        breakpoint:
            cfg.Save();
            Console.ReadKey();
        }
    }
}
