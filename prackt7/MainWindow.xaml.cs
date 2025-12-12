using prackt7.models;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace prackt7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int id = 0;

        int allDocs = 0;

        int allPacient = 0;

        List<int> docsId = new List<int>();
        List<int> pacientsId = new List<int>();

        Doc currDoctor = new Doc();
        Pacient currPacient = new Pacient();
        Pacient lastInfo = new Pacient();

        bool isLogined = false;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Form_Initialized(object sender, EventArgs e)
        {
            try
            {
                string directoryPath = @"C:\Users\Neven\Desktop\Ucheba\TRPO\prackt7\prackt7\bin\Debug\net8.0-windows";
                string[] jsonFiles = Directory.GetFiles(directoryPath, "*.json");

                foreach (string filePath in jsonFiles)
                {
                    string[] strings = filePath.Split('.');
                    string[] strings2 = strings[1].Split('-');
                    string[] strings3 = strings2[1].Split(@"\");
                    string[] strings4 = strings3[1].Split("_");

                    if (strings4[0] == "D")
                    {
                        try
                        {
                            if (int.TryParse(strings4[1], out int id))
                            {
                                docsId.Add(id);
                            }
                        }
                        catch
                        {

                        }

                    }

                    else if (strings4[0] == "P")
                    {
                        try
                        {
                            if (int.TryParse(strings4[1], out int id))
                            {
                                pacientsId.Add(id);
                            }
                        }
                        catch
                        {

                        }


                    }
                }
                allDocs = docsId.Count;
                allPacient = pacientsId.Count;
                Counts.Text = $"{allDocs} Пользователей, {allPacient} Пациентов";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }


        private void register_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(reg_Name.Text) && !string.IsNullOrEmpty(reg_LastName.Text) && !string.IsNullOrEmpty(reg_MiddleName.Text) && !string.IsNullOrEmpty(reg_Password.Text) && !string.IsNullOrEmpty(reg_ConfirmPassword.Text) && !string.IsNullOrEmpty(reg_Specialisation.Text))
            {
                if (reg_ConfirmPassword.Text == reg_Password.Text)
                {
                    Doc doc = new Doc()
                    {
                        Name = reg_Name.Text,
                        LastName = reg_LastName.Text,
                        MiddleName = reg_MiddleName.Text,
                        Password = reg_Password.Text,
                        Spectialisation = reg_Specialisation.Text,
                    };

                    var docJson = JsonSerializer.Serialize(doc);

                    while (true)
                    {
                        Random rnd = new Random();

                        id = rnd.Next(0, 99999);
                        if (!docsId.Any(i => i == id))
                        {

                            using (StreamWriter sw = new StreamWriter($"D_{id}.json"))
                            {
                                sw.WriteLine(docJson);
                            }

                            break;
                        }
                    }
                    allDocs = docsId.Count;
                    allPacient = pacientsId.Count;
                    Counts.Text = $"{allDocs} Пользователей, {allPacient} Пациентов";

                }
                else MessageBox.Show("Пароли не совпадают");

            }
            else MessageBox.Show("Не все поля заполнены");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Log_Pass.Text) && !string.IsNullOrEmpty(Log_Id.Text))
            {
                try
                {
                    using (StreamReader sr = new StreamReader($"D_{Log_Id.Text}.json"))
                    {
                        currDoctor = JsonSerializer.Deserialize<Doc>(sr.ReadLine());
                    }

                    if (currDoctor.Password == Log_Pass.Text)
                    {
                        Curr_Id.Content = Log_Id.Text;
                        Curr_Name.Content = currDoctor.Name;
                        Curr_LastName.Content = currDoctor.LastName;
                        Curr_MiddleName.Content = currDoctor.MiddleName;
                        Curr_Specialisation.Content = currDoctor.Spectialisation;
                        isLogined = true;
                    }
                    else
                    {
                        MessageBox.Show("Пароль неверный");
                        Curr_Id.Content = "";
                        Curr_Name.Content = "";
                        Curr_LastName.Content = "";
                        Curr_MiddleName.Content = "";
                        Curr_Specialisation.Content = "";
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else MessageBox.Show("Не все поля заполнены");
        }

        private void AddPacient_Button_Click(object sender, RoutedEventArgs e)
        {
            if (isLogined)
            {
                addPacient();
            }
            else
            {
                MessageBox.Show("Вы не авторизованы!");
            }
        }


        private void EditPacient_Button_Click(object sender, RoutedEventArgs e)
        {
            if (isLogined)
            {
                Edit();
            }
            else
            {
                MessageBox.Show("Вы не авторизованы!");
            }

        }

        private void SearchPacient_Button_Click(object sender, RoutedEventArgs e)
        {
            if (isLogined)
            {
                SearchPacient(Search_TextBox.Text, true);
            }
            else
            {
                MessageBox.Show("Вы не авторизованы!");
            }

        }

        private void Recovery_Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter($"P_{lastInfo.Id}.json"))
            {
                sw.WriteLine(JsonSerializer.Serialize(lastInfo));
            }
            SearchPacient(lastInfo.Id.ToString(), false);
        }


        void addPacient()
        {
            try
            {
                if (!string.IsNullOrEmpty(LastName_Add.Text) && !string.IsNullOrEmpty(Name_Add.Text) && !string.IsNullOrEmpty(MiddleName_Add.Text) && !string.IsNullOrEmpty(Date_Add.Text))
                {
                    while (true)
                    {
                        Random rnd = new Random();

                        id = rnd.Next(0, 99999);
                        if (!docsId.Any(i => i == id))
                        {

                            Pacient p = new Pacient()
                            {
                                Name = Name_Add.Text,
                                LastName = LastName_Add.Text,
                                MiddleName = MiddleName_Add.Text,
                                Birthday = Date_Add.Text,
                            };


                            using (StreamWriter sw = new StreamWriter($"P_{id}.json"))
                            {
                                sw.WriteLine(JsonSerializer.Serialize(p));
                            }
                            break;

                        }

                    }
                    allDocs = docsId.Count;
                    allPacient = pacientsId.Count;
                    Counts.Text = $"{allDocs} Пользователей, {allPacient} Пациентов";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        void SearchPacient(string id, bool updateLastInfo)
        {
            if (!string.IsNullOrEmpty(Search_TextBox.Text))
            {
                try
                {
                    using (StreamReader sr = new StreamReader($"P_{id}.json"))
                    {
                        currPacient = JsonSerializer.Deserialize<Pacient>(sr.ReadLine());
                    }
                    if (updateLastInfo)
                    {
                        lastInfo = currPacient;
                    }
                    currPacient.Id = int.Parse(id);
                    Pacient_id.Content = currPacient.Id;
                    Name.Content = currPacient.Name;
                    LastName.Content = currPacient.LastName;
                    MiddleName.Content = currPacient.MiddleName;
                    Birthday.Content = currPacient.Birthday;
                    LastAppointment.Content = currPacient.LastAppointment ?? "Н/Д";
                    LastDoctor.Content = currPacient.LastDoctor ?? "Н/Д";
                    Diagnosis.Content = currPacient.Diagnosis ?? "Н/Д";
                    Recomendations.Content = currPacient.Recomendations ?? "Н/Д";

                    if (LastAppointment.Content == "") LastAppointment.Content = "Н/Д";
                    if (LastDoctor.Content == "") LastDoctor.Content = "Н/Д";
                    if (Diagnosis.Content == "") Diagnosis.Content = "Н/Д";
                    if (Recomendations.Content == "") Recomendations.Content = "Н/Д";




                    Name_Edit.Text = currPacient.Name;
                    LastName_Edit.Text = currPacient.LastName;
                    MiddleName_Edit.Text = currPacient.MiddleName;
                    Date_Edit.Text = currPacient.Birthday;
                    Diagnosis_Edit.Text = currPacient.Diagnosis;
                    Recomendations_Edit.Text = currPacient.Recomendations;

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                MessageBox.Show("Поле незаполнено");
            }
        }


        void Edit()
        {
            try
            {
                if (!string.IsNullOrEmpty(LastName_Edit.Text) && !string.IsNullOrEmpty(Name_Edit.Text) && !string.IsNullOrEmpty(MiddleName_Edit.Text) && !string.IsNullOrEmpty(Date_Edit.Text))
                {
                    currPacient.Name = Name_Edit.Text;
                    currPacient.LastName = LastName_Edit.Text;
                    currPacient.MiddleName = MiddleName_Edit.Text;
                    currPacient.Birthday = Date_Edit.Text;
                    currPacient.Diagnosis = Diagnosis_Edit.Text;
                    currPacient.Recomendations = Recomendations_Edit.Text;
                    currPacient.LastDoctor = Curr_Id.Content.ToString();
                    currPacient.LastAppointment = DateTime.Now.ToString();

                    using (StreamWriter sw = new StreamWriter($"P_{currPacient.Id}.json"))
                    {
                        sw.WriteLine(JsonSerializer.Serialize(currPacient));
                    }
                    SearchPacient(currPacient.Id.ToString(), false);

                }
                else
                {
                    MessageBox.Show("Не все поля заполнены");
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


    }
}