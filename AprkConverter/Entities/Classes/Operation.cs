using System.Collections.Generic;

namespace AprkConverter.Entities.Classes {

    public class Operation {

        public string NameOper {
            get; private set;
        }
        public short IndexOper {
            get; private set;
        }
        public Sketch Sketch {
            get; set;
        }

        public List<Step> Steps {
            get; private set;
        }

        public Operation(string nameOper, short indexOper) {
            NameOper = nameOper;
            IndexOper = indexOper;
            Steps = new List<Step>();
        }

        public void AddStep(Step step) {
            Steps.Add(step);
        }
    }
}
