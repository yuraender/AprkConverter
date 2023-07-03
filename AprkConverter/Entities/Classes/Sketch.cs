namespace AprkConverter.Entities.Classes {

    public class Sketch {

        public string Caption {
            get; private set;
        }
        public string KompasFile {
            get; private set;
        }

        public Sketch(string caption, string kompasFile) {
            Caption = caption;
            KompasFile = kompasFile;
        }
    }
}
