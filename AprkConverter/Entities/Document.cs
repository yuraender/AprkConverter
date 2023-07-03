using AprkConverter.Entities.Classes;
using System.Collections.Generic;
using System.Text;

namespace AprkConverter.Entities {

    public class Document {

        public List<Drawing> Drawings {
            get; private set;
        }
        public List<Operation> Operations {
            get; private set;
        }

        public Document() {
            Drawings = new List<Drawing>();
            Operations = new List<Operation>();
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Drawings.Count; i++) {
                Drawing drawing = Drawings[i];
                sb.Append($"Drawing{i + 1}").Append('\n');
                sb.Append("\tcaption = ").Append(drawing.Caption).Append('\n');
                sb.Append("\tkompasfile = ").Append(drawing.KompasFile).Append('\n');
            }
            for (int i = 0; i < Operations.Count; i++) {
                Operation operation = Operations[i];
                sb.Append($"Operation{i + 1}").Append('\n');
                sb.Append("\tnameoper = ").Append(operation.NameOper).Append('\n');
                sb.Append("\tindexoper = ").Append(operation.IndexOper).Append('\n');
                if (operation.Sketch != null) {
                    sb.Append($"\tSketch").Append('\n');
                    sb.Append("\t\tcaption = ").Append(operation.Sketch.Caption).Append('\n');
                    sb.Append("\t\tkompasfile = ").Append(operation.Sketch.KompasFile).Append('\n');
                }
                for (int j = 0; j < operation.Steps.Count; j++) {
                    Step step = operation.Steps[j];
                    sb.Append($"\tStep{j + 1}").Append('\n');
                    sb.Append("\t\tname = ").Append(step.Name).Append('\n');
                    sb.Append("\t\tnumstep = ").Append(step.NumStep).Append('\n');
                    for (int k = 0; k < step.Parameters.Count; k++) {
                        Parameter parameter = step.Parameters[k];
                        sb.Append($"\t\tParameter{k + 1}").Append('\n');
                        sb.Append("\t\t\tparam = ").Append(parameter.Param).Append('\n');
                        sb.Append("\t\t\tdimVAL = ").Append(parameter.DimVal).Append('\n');
                        if (parameter.UpDev != null) {
                            sb.Append("\t\t\tvalue_dimVAL = ").Append(parameter.UpDev).Append('\n');
                        }
                        if (parameter.LowDev != null) {
                            sb.Append("\t\t\tvalue_lowDEV = ").Append(parameter.LowDev).Append('\n');
                        }
                        if (parameter.Qual != null) {
                            sb.Append("\t\t\tvalue_QUAL = ").Append(parameter.Qual).Append('\n');
                        }
                    }
                }
            }
            return sb.ToString();
        }
    }
}
