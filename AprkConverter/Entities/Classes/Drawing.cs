namespace AprkConverter.Entities.Classes {

    public class Drawing {

        public string Caption {
            get; private set;
        }
        public string KompasFile {
            get; private set;
        }

        public Drawing(string caption, string kompasFile) {
            Caption = caption;
            KompasFile = kompasFile;
        }
    }
}
