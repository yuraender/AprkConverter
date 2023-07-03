using AprkConverter.Entities.Classes;
using AprkConverter.Utils;
using IniParser.Model;
using System.Linq;

namespace AprkConverter.Entities {

    public class Project {

        private IniData _data;

        public string Path {
            get; private set;
        }

        public Project(IniData data, string path) {
            _data = data;
            Path = path;
        }

        public Document Parse() {
            Document document = new Document();

            foreach (KeyData keyData in _data["dimsPARAM"]) {
                string cadFile = AProposUtil.GetDimsParameter<string>(_data, keyData.KeyName, "CADfile");
                string kompasFile = Path + "\\" + cadFile + ".cdw";
                if (document.Drawings.Any(d => d.KompasFile == kompasFile)) {
                    continue;
                }
                document.Drawings.Add(new Drawing("Деталь", kompasFile));
                break;
            }
            foreach (KeyData keyData in _data["dimsPARAM.T"]) {
                short operId = AProposUtil.GetDimsParameterT<short>(_data, keyData.KeyName, "operID");
                string cadFile = AProposUtil.GetDimsParameterT<string>(_data, keyData.KeyName, "CADfile");
                string kompasFile = Path + "\\" + cadFile + ".frw";
                if (operId != 0 || string.IsNullOrEmpty(cadFile)
                    || document.Drawings.Any(d => d.KompasFile == kompasFile)) {
                    continue;
                }
                document.Drawings.Add(new Drawing("Заготовка", kompasFile));
            }

            foreach (KeyData keyData in _data["dimsPARAM.T"]) {
                short operId = AProposUtil.GetDimsParameterT<short>(_data, keyData.KeyName, "operID");
                if (operId == 0) {
                    continue;
                }

                Operation operation;
                if (!document.Operations.Any(o => o.IndexOper == operId)) {
                    operation = new Operation(
                        AProposUtil.GetOpAlias(_data, operId.ToString()),
                        operId
                    );
                    document.Operations.Add(operation);
                } else {
                    operation = document.Operations.Where(o => o.IndexOper == operId).First();
                }

                string cadFile = AProposUtil.GetDimsParameterT<string>(_data, keyData.KeyName, "CADfile");
                if (!string.IsNullOrEmpty(cadFile) && operation.Sketch == null) {
                    string kompasFile = Path + "/" + cadFile + ".frw";
                    operation.Sketch = new Sketch("Эскиз", kompasFile);
                }

                // TODO: Probably had to fix
                //bool allStepsBlank = true;
                //foreach (KeyData kd in _data["dimsPARAM.T"]) {
                //    if (AProposUtil.GetDimsParameterT<short>(_data, kd.KeyName, "operID") != operation.IndexOper) {
                //        continue;
                //    }
                //    if (!string.IsNullOrEmpty(
                //        AProposUtil.GetDimsParameterT<string>(_data, kd.KeyName, "suboperSEQ")
                //    )) {
                //        allStepsBlank = false;
                //        break;
                //    }
                //}

                string suboperSeq = AProposUtil.GetDimsParameterT<string>(_data, keyData.KeyName, "suboperSEQ");
                if (string.IsNullOrEmpty(suboperSeq)) {
                    suboperSeq = operation.IndexOper.ToString();
                }
                short numStep = short.Parse(suboperSeq);

                Step step;
                if (!operation.Steps.Any(s => s.NumStep == numStep)) {
                    step = new Step(
                        AProposUtil.GetStAlias(_data, numStep.ToString()),
                        numStep
                    );
                    operation.Steps.Add(step);
                } else {
                    step = operation.Steps.Where(s => s.NumStep == numStep).First();
                }

                Parameter parameter = new Parameter(
                    true, true,
                    AProposUtil.GetDimsParameterT<string>(_data, keyData.KeyName, "TAG"),
                    AProposUtil.GetDimsParameterT<string>(_data, keyData.KeyName, "dimVAL"),
                    AProposUtil.GetDimsParameterT<string>(_data, keyData.KeyName, "CADID")
                );
                string upDev = AProposUtil.GetDimsParameterT<string>(_data, keyData.KeyName, "upDEV");
                string lowDev = AProposUtil.GetDimsParameterT<string>(_data, keyData.KeyName, "lowDEV");
                string qual = AProposUtil.GetDimsParameterT<string>(_data, keyData.KeyName, "QUAL");
                if (string.IsNullOrEmpty(upDev) && string.IsNullOrEmpty(lowDev)) {
                    if (short.TryParse(qual, out short qualS)) {
                        parameter.Qual = qualS;
                    }
                } else if (!string.IsNullOrEmpty(upDev) && !string.IsNullOrEmpty(lowDev)) {
                    if (short.TryParse(upDev, out short upDevS)) {
                        parameter.UpDev = upDevS;
                    }
                    if (short.TryParse(lowDev, out short lowDevS)) {
                        parameter.LowDev = lowDevS;
                    }
                }
                step.AddParameter(parameter);
            }

            return document;
        }
    }
}
