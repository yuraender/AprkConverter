using AprkConverter.Entities;
using IniParser;
using IniParser.Exceptions;
using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;

namespace AprkConverter.Windows {

    public partial class MainWindow : Window {

        private Project _project;

        private void SaveButton_Click(object sender, RoutedEventArgs e) {
            Document document = _project.Parse();
            SaveDocument(document);
            MessageBox.Show("Successfully converted and saved!");
        }

        private void OpenProjectMenuItem_Click(object sender, RoutedEventArgs e) {
            _project = LoadProject();
            if (_project == null) {
                return;
            }
            ProjectTextBlock.Visibility = Visibility.Hidden;
            SaveButton.Visibility = Visibility.Visible;
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        private Project LoadProject() {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файл проекта A'PROPOS|*.APRk; *.APRx";
            if (openFileDialog.ShowDialog().Value) {
                FileIniDataParser parser = new FileIniDataParser();
                int codepage;
                try {
                    if (!int.TryParse(
                        parser.ReadFile(openFileDialog.FileName)["COMMONDATA"]["CODEPAGE"],
                        out codepage
                    )) {
                        MessageBox.Show("Файл не является APRk-файлом.");
                        return null;
                    }
                } catch (ParsingException) {
                    MessageBox.Show("Файл не является INI-файлом.");
                    return null;
                }
                return new Project(
                    parser.ReadFile(openFileDialog.FileName, Encoding.GetEncoding(codepage)),
                    Path.GetDirectoryName(openFileDialog.FileName)
                );
            }
            return null;
        }

        private void SaveDocument(Document document) {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            // TODO: Extension is unknown
            saveFileDialog.Filter = "Документ ВЕРТИКАЛЬ|*.*";
            if (saveFileDialog.ShowDialog().Value) {
                File.WriteAllText(saveFileDialog.FileName, document.ToString());
            }
        }
    }
}
