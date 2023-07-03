using IniParser.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AprkConverter.Utils {

    public static class AProposUtil {

        private static Dictionary<string, string> Names = new Dictionary<string, string> {
            { "ZAG", "" }, { "MO1", "mex" }, { "TO1", "trm" },
            { "MO2", "mex" }, { "XTO", "trm" }, { "MOxto", "mex" },
            { "TO2", "trm" }, { "MO3", "mex" }, { "COAT", "pok" },
            { "MOcoat", "mex" }, { "MO4", "mex" }, { "GALV", "pok" }
        };

        public static T GetDimsParameter<T>(IniData data, string operation, string parameter) {
            string[] head = data["DIMS"]["dimsHEAD"]
                .Split(',')
                .Select(p => p.Trim())
                .ToArray();
            if (!head.Contains(parameter)) {
                return default(T);
            }

            int index = Array.FindIndex(head, p => p == parameter);
            string[] parameters = data["dimsPARAM"][operation]
                .Split(',')
                .Select(p => p.Trim())
                .ToArray();
            return (T) Convert.ChangeType(parameters[index], typeof(T));
        }

        public static T GetDimsParameterT<T>(IniData data, string operation, string parameter) {
            string[] head = data["DIMS.T"]["dimsHEAD"]
                .Split(',')
                .Select(p => p.Trim())
                .ToArray();
            if (!head.Contains(parameter)) {
                return default(T);
            }

            int index = Array.FindIndex(head, p => p == parameter);
            string[] parameters = data["dimsPARAM.T"][operation]
                .Split(',')
                .Select(p => p.Trim())
                .ToArray();
            return (T) Convert.ChangeType(parameters[index], typeof(T));
        }

        // Name replace methods
        public static string GetOpAlias(IniData data, string operation) {
            return Names[GetEtPAlias(data, operation)] + "_oper";
        }

        public static string GetStAlias(IniData data, string step) {
            return Names[GetEtPAlias(data, step)] + "_step";
        }


        private static string GetEtPAlias(IniData data, string operation) {
            string[] operations = data["GRAPHIT-TM"]["ETOPERATIONSLAYOUT"]
                .Split(',')
                .Select(o => o.Trim())
                .ToArray();
            int index = Array.FindIndex(operations, o => o == operation);

            string[] aliases = data["GRAPHIT-TM"]["ETPALIAS"]
                .Split(',')
                .Select(p => p.Trim())
                .ToArray();
            return aliases[index];
        }
    }
}
