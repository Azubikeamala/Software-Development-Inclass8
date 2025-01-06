using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace InClass8AA
{

    // In-Class 8
    // Amalachukwu Azubike
    // Instructor: Sukhbir Tatla.
    public partial class MainWindow : Window
    {
        private readonly List<Student> students = new List<Student>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReg.Text) ||
                string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(txtCity.Text) ||
                string.IsNullOrWhiteSpace(txtPostalCode.Text) ||
                string.IsNullOrWhiteSpace(txtProvince.Text) ||
                string.IsNullOrWhiteSpace(txtProgram.Text) ||
                string.IsNullOrWhiteSpace(txtGPA.Text))
            {
                MessageBox.Show("All fields are required.");
                return;
            }

            if (!double.TryParse(txtGPA.Text, out var gpa) || gpa < 0 || gpa > 4)
            {
                MessageBox.Show("GPA must be between 0 and 4.");
                return;
            }

            var student = new Student
            {
                RegistrationNumber = txtReg.Text,
                Name = txtName.Text,
                StreetAddress = txtAddress.Text,
                City = txtCity.Text,
                PostalCode = txtPostalCode.Text,
                Province = txtProvince.Text,
                Program = txtProgram.Text,
                GPA = gpa
            };

            if (students.Any(s => s.RegistrationNumber == student.RegistrationNumber))
            {
                MessageBox.Show("Registration number is unique, duplicate is not allowed.");
            }
            else
            {
                students.Add(student);
                MessageBox.Show("Student added successfully.");
                ClearFields();
                RefreshDataGrid();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var student = students.FirstOrDefault(s => s.RegistrationNumber == txtReg.Text);
            if (student != null)
            {
                if (!double.TryParse(txtGPA.Text, out var gpa) || gpa < 0 || gpa > 4)
                {
                    MessageBox.Show("GPA must be between 0 and 4.");
                    return;
                }

                student.Name = txtName.Text;
                student.StreetAddress = txtAddress.Text;
                student.City = txtCity.Text;
                student.PostalCode = txtPostalCode.Text;
                student.Province = txtProvince.Text;
                student.Program = txtProgram.Text;
                student.GPA = gpa;

                MessageBox.Show("Student updated successfully.");
                ClearFields();
                RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("Student not found.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var student = students.FirstOrDefault(s => s.RegistrationNumber == txtReg.Text);
            if (student != null)
            {
                students.Remove(student);
                MessageBox.Show("Student deleted successfully.");
                ClearFields();
                RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("Student not found.");
            }
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            var student = students.FirstOrDefault(s => s.RegistrationNumber == txtFind.Text);
            if (student != null)
            {
                txtReg.Text = student.RegistrationNumber;
                txtName.Text = student.Name;
                txtAddress.Text = student.StreetAddress;
                txtCity.Text = student.City;
                txtPostalCode.Text = student.PostalCode;
                txtProvince.Text = student.Province;
                txtProgram.Text = student.Program;
                txtGPA.Text = student.GPA.ToString();
            }
            else
            {
                MessageBox.Show("Student not found.");
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem is Student selectedStudent)
            {
                txtReg.Text = selectedStudent.RegistrationNumber;
                txtName.Text = selectedStudent.Name;
                txtAddress.Text = selectedStudent.StreetAddress;
                txtCity.Text = selectedStudent.City;
                txtPostalCode.Text = selectedStudent.PostalCode;
                txtProvince.Text = selectedStudent.Province;
                txtProgram.Text = selectedStudent.Program;
                txtGPA.Text = selectedStudent.GPA.ToString();
            }
        }

        private void ClearFields()
        {
            txtReg.Clear();
            txtName.Clear();
            txtAddress.Clear();
            txtCity.Clear();
            txtPostalCode.Clear();
            txtProvince.Clear();
            txtProgram.Clear();
            txtGPA.Clear();
        }

        private void RefreshDataGrid()
        {
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = students;
        }
    }
}