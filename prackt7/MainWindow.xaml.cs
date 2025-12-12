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

        Pacient lastInfo = new Pacient();

        private Pacient currentPacient = new Pacient();
        private Pacient addPacientM = new Pacient();

        private Doc currentDoc = new Doc();

        private Doc enterDoc = new Doc();

        private Doc newDoc = new Doc();



        bool isLogined = false;


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
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

                    var docJson = JsonSerializer.Serialize(newDoc);

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
            if (!string.IsNullOrEmpty(enterDoc.Password))
            {
                try
                {
                    using (StreamReader sr = new StreamReader($"D_{Log_Id.Text}.json"))
                    {
                        currentDoc = JsonSerializer.Deserialize<Doc>(sr.ReadLine());
                    }

                    if (currentDoc.Password == Log_Pass.Text)
                    {
                        isLogined = true;
                        Curr_Id.Content = Log_Id.Text;
                        MessageBox.Show(currentDoc.LastName.ToString());
                        viewDoc.DataContext = currentDoc;
                    }
                    else
                    {
                        MessageBox.Show("Пароль неверный");
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
                SearchPacient(currentPacient.Id.ToString(), true);
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
            ResetUser();
            SearchPacient(lastInfo.Id.ToString(), false);
        }

        private void ResetUser()
        {
        }


        void addPacient()
        {
            addForm.DataContext = addPacientM;

            try
            {
                if (!string.IsNullOrEmpty(addPacientM.LastName) && !string.IsNullOrEmpty(addPacientM.Name) && !string.IsNullOrEmpty(addPacientM.MiddleName) && !string.IsNullOrEmpty(addPacientM.Birthday))
                {
                    while (true)
                    {
                        Random rnd = new Random();

                        id = rnd.Next(0, 99999);
                        if (!docsId.Any(i => i == id))
                        {

                            addPacientM.Id = id;
                            using (StreamWriter sw = new StreamWriter($"P_{id}.json"))
                            {
                                sw.WriteLine(JsonSerializer.Serialize(addPacientM));
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
                        currentPacient = JsonSerializer.Deserialize<Pacient>(sr.ReadLine());
                    }
                    if (updateLastInfo)
                    {
                        lastInfo = currentPacient;
                    }

                    viewPacient.DataContext = currentPacient;
                    editForm.DataContext = currentPacient;


                    if (LastAppointment.Content == "") LastAppointment.Content = "Н/Д";
                    if (LastDoctor.Content == "") LastDoctor.Content = "Н/Д";
                    if (Diagnosis.Content == "") Diagnosis.Content = "Н/Д";
                    if (Recomendations.Content == "") Recomendations.Content = "Н/Д";



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
                    currentPacient.LastDoctor = Curr_Id.Content.ToString();
                    currentPacient.LastAppointment = DateTime.Now.ToString();

                    using (StreamWriter sw = new StreamWriter($"P_{currentPacient.Id}.json"))
                    {
                        sw.WriteLine(JsonSerializer.Serialize(currentPacient));
                    }
                    SearchPacient(currentPacient.Id.ToString(), false);

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

        private void registerForm_Initialized(object sender, EventArgs e)
        {
            registerForm.DataContext = newDoc;
        }

        private void editForm_Initialized(object sender, EventArgs e)
        {
            editForm.DataContext = currentPacient;
        }

        private void addForm_Initialized(object sender, EventArgs e)
        {
            addForm.DataContext = addPacientM;
        }

        private void viewPacient_Initialized(object sender, EventArgs e)
        {
            viewPacient.DataContext = currentPacient;
        }

        private void searchForm_Initialized(object sender, EventArgs e)
        {
            searchForm.DataContext = currentPacient;
        }

        private void viewDoc_Initialized(object sender, EventArgs e)
        {
            viewDoc.DataContext = currentDoc;
        }

        private void enterForm_Initialized(object sender, EventArgs e)
        {
            enterForm.DataContext = enterDoc;
        }
    }
}