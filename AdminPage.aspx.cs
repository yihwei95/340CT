using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

public partial class Admin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            AddStudentTab.CssClass = "Clicked";
            MainView.ActiveViewIndex = 0;
            DisplayStudentGridView();
            DisplayLecturerGridView();
            DisplaySubjectGridView();
        }
    }

    protected void AddStudentTab_Click(object sender, EventArgs e)
    {
        AddStudentTab.CssClass = "Clicked";
        AddLecturerTab.CssClass = "Initial";
        AddSubjectTab.CssClass = "Initial";
        AddLecturerSubjectTab.CssClass = "Initial";
        RemoveEnrollmentTab.CssClass = "Initial";
        DeleteCategoryTab.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }

    protected void AddLecturerTab_Click(object sender, EventArgs e)
    {
        AddStudentTab.CssClass = "Initial";
        AddLecturerTab.CssClass = "Clicked";
        AddSubjectTab.CssClass = "Initial";
        AddLecturerSubjectTab.CssClass = "Initial";
        RemoveEnrollmentTab.CssClass = "Initial";
        DeleteCategoryTab.CssClass = "Initial";
        MainView.ActiveViewIndex = 1;
    }

    protected void AddSubjectTab_Click(object sender, EventArgs e)
    {
        AddStudentTab.CssClass = "Initial";
        AddLecturerTab.CssClass = "Initial";
        AddSubjectTab.CssClass = "Clicked";
        AddLecturerSubjectTab.CssClass = "Initial";
        RemoveEnrollmentTab.CssClass = "Initial";
        DeleteCategoryTab.CssClass = "Initial";
        MainView.ActiveViewIndex = 2;
    }

    protected void AddLecturerSubjectTab_Click(object sender, EventArgs e)
    {
        AddStudentTab.CssClass = "Initial";
        AddLecturerTab.CssClass = "Initial";
        AddSubjectTab.CssClass = "Initial";
        AddLecturerSubjectTab.CssClass = "Clicked";
        RemoveEnrollmentTab.CssClass = "Initial";
        DeleteCategoryTab.CssClass = "Initial";
        MainView.ActiveViewIndex = 3;
    }

    protected void RemoveEnrollmentTab_Click(object sender, EventArgs e)
    {
        AddStudentTab.CssClass = "Initial";
        AddLecturerTab.CssClass = "Initial";
        AddSubjectTab.CssClass = "Initial";
        AddLecturerSubjectTab.CssClass = "Initial";
        RemoveEnrollmentTab.CssClass = "Clicked";
        DeleteCategoryTab.CssClass = "Initial";
        MainView.ActiveViewIndex = 4;
    }

    protected void DeleteCategoryTab_Click(object sender, EventArgs e)
    {
        AddStudentTab.CssClass = "Initial";
        AddLecturerTab.CssClass = "Initial";
        AddSubjectTab.CssClass = "Initial";
        AddLecturerSubjectTab.CssClass = "Initial";
        RemoveEnrollmentTab.CssClass = "Initial";
        DeleteCategoryTab.CssClass = "Clicked";
        MainView.ActiveViewIndex = 5;
    }

    //AddStudent
    private void DisplayStudentGridView()
    {
        DataTable dataTableEmpty = new DataTable();
        DataRow dataRowEmpty = null;
        dataTableEmpty.Columns.Add(new DataColumn("RowNumber"));
        dataTableEmpty.Columns.Add(new DataColumn("StudentName"));
        dataTableEmpty.Columns.Add(new DataColumn("StudentID"));
        dataTableEmpty.Columns.Add(new DataColumn("StudentAddress"));
        dataTableEmpty.Columns.Add(new DataColumn("StudentPNumber"));
        dataTableEmpty.Columns.Add(new DataColumn("StudentEAddress"));
        dataTableEmpty.Columns.Add(new DataColumn("StudentCourse"));
        dataTableEmpty.Columns.Add(new DataColumn("ParentID"));

        dataRowEmpty = dataTableEmpty.NewRow();
        dataRowEmpty["RowNumber"] = 1;
        dataRowEmpty["StudentName"] = "";
        dataRowEmpty["StudentID"] = "";
        dataRowEmpty["StudentAddress"] = "";
        dataRowEmpty["StudentPNumber"] = "";
        dataRowEmpty["StudentEAddress"] = "";
        dataRowEmpty["StudentCourse"] = "";
        dataRowEmpty["ParentID"] = "";
        dataTableEmpty.Rows.Add(dataRowEmpty);

        ViewState["StudentGridView"] = dataTableEmpty;
        StudentGridView.DataSource = dataTableEmpty;
        StudentGridView.DataBind();
    }

    private DataSet GetData(string query)
    {
        string conStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
        SqlCommand sqlCommand = new SqlCommand(query);
        using (SqlConnection sqlConnection = new SqlConnection(conStr))
        {
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
            {
                sqlCommand.Connection = sqlConnection;
                sqlDataAdapter.SelectCommand = sqlCommand;
                using (DataSet dataSet = new DataSet())
                {
                    sqlDataAdapter.Fill(dataSet);
                    return dataSet;
                }
            }
        }
    }

    protected void OnRowDataBoundStudent(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList dropDownListStudent = (e.Row.FindControl("StudentCourse") as DropDownList);
            dropDownListStudent.DataSource = GetData("SELECT COURSEID + ' ' + COURSENAME AS COURSE1 FROM COURSE;");
            dropDownListStudent.DataTextField = "COURSE1";
            dropDownListStudent.DataValueField = "COURSE1";
            dropDownListStudent.DataBind();

            dropDownListStudent.Items.Insert(0, "-- Select Course --");
        }
    }

    private void SetPreviousDataStudent()
    {
        int rowIndex = 0;
        if (ViewState["StudentGridView"] != null)
        {
            DataTable dataTableCurrent = (DataTable)ViewState["StudentGridView"];
            if (dataTableCurrent.Rows.Count > 0)
            {
                for (int i = 0; i < dataTableCurrent.Rows.Count; i++)
                {
                    TextBox textBoxSName = (TextBox)StudentGridView.Rows[rowIndex].Cells[1].FindControl("StudentName");
                    TextBox textBoxSID = (TextBox)StudentGridView.Rows[rowIndex].Cells[2].FindControl("StudentID");
                    TextBox textBoxSAdd = (TextBox)StudentGridView.Rows[rowIndex].Cells[3].FindControl("StudentAddress");
                    TextBox textBoxSPNumber = (TextBox)StudentGridView.Rows[rowIndex].Cells[4].FindControl("StudentPNumber");
                    TextBox textBoxSEAdd = (TextBox)StudentGridView.Rows[rowIndex].Cells[5].FindControl("StudentEAddress");
                    DropDownList dropDownListSCourse = (DropDownList)StudentGridView.Rows[rowIndex].Cells[6].FindControl("StudentCourse");
                    TextBox textBoxPID = (TextBox)StudentGridView.Rows[rowIndex].Cells[7].FindControl("ParentID");

                    StudentGridView.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    textBoxSName.Text = dataTableCurrent.Rows[i]["StudentName"].ToString();
                    textBoxSID.Text = dataTableCurrent.Rows[i]["StudentID"].ToString();
                    textBoxSAdd.Text = dataTableCurrent.Rows[i]["StudentAddress"].ToString();
                    textBoxSPNumber.Text = dataTableCurrent.Rows[i]["StudentPNumber"].ToString();
                    textBoxSEAdd.Text = dataTableCurrent.Rows[i]["StudentEAddress"].ToString();
                    dropDownListSCourse.SelectedValue = dataTableCurrent.Rows[i]["StudentCourse"].ToString();
                    textBoxPID.Text = dataTableCurrent.Rows[i]["ParentID"].ToString();
                    rowIndex++;
                }
            }
        }
    }

    private void AddNewRowToStudentGV()
    {
        int rowIndex = 0;
        if (ViewState["StudentGridView"] != null)
        {
            DataTable dataTableCurrent = (DataTable)ViewState["StudentGridView"];
            DataRow dataRowCurrent = null;
            if (dataTableCurrent.Rows.Count > 0)
            {
                for (int i = 1; i <= dataTableCurrent.Rows.Count; i++)
                {
                    TextBox textBoxSName = (TextBox)StudentGridView.Rows[rowIndex].Cells[1].FindControl("StudentName");
                    TextBox textBoxSID = (TextBox)StudentGridView.Rows[rowIndex].Cells[2].FindControl("StudentID");
                    TextBox textBoxSAdd = (TextBox)StudentGridView.Rows[rowIndex].Cells[3].FindControl("StudentAddress");
                    TextBox textBoxSPNumber = (TextBox)StudentGridView.Rows[rowIndex].Cells[4].FindControl("StudentPNumber");
                    TextBox textBoxSEAdd = (TextBox)StudentGridView.Rows[rowIndex].Cells[5].FindControl("StudentEAddress");
                    DropDownList dropDownListSCourse = (DropDownList)StudentGridView.Rows[rowIndex].Cells[6].FindControl("StudentCourse");
                    TextBox textBoxPID = (TextBox)StudentGridView.Rows[rowIndex].Cells[7].FindControl("ParentID");

                    dataRowCurrent = dataTableCurrent.NewRow();
                    dataRowCurrent["RowNumber"] = i + 1;
                    dataTableCurrent.Rows[i - 1]["StudentName"] = textBoxSName.Text;
                    dataTableCurrent.Rows[i - 1]["StudentID"] = textBoxSID.Text;
                    dataTableCurrent.Rows[i - 1]["StudentAddress"] = textBoxSAdd.Text;
                    dataTableCurrent.Rows[i - 1]["StudentPNumber"] = textBoxSPNumber.Text;
                    dataTableCurrent.Rows[i - 1]["StudentEAddress"] = textBoxSEAdd.Text;
                    dataTableCurrent.Rows[i - 1]["StudentCourse"] = dropDownListSCourse.SelectedValue;
                    dataTableCurrent.Rows[i - 1]["ParentID"] = textBoxPID.Text;

                    rowIndex++;
                }
                dataTableCurrent.Rows.Add(dataRowCurrent);
                ViewState["StudentGridView"] = dataTableCurrent;

                StudentGridView.DataSource = dataTableCurrent;
                StudentGridView.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null.");
        }
        SetPreviousDataStudent();
    }

    protected void ButtonAddNewStudent_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            AddNewRowToStudentGV();
        }
    }

    private void SetRowDataStudent()
    {
        int rowIndex = 0;
        if (ViewState["StudentGridView"] != null)
        {
            DataTable dataTableCurrent = (DataTable)ViewState["StudentGridView"];
            DataRow dataRowCurrent = null;
            if (dataTableCurrent.Rows.Count > 0)
            {
                for (int i = 1; i <= dataTableCurrent.Rows.Count; i++)
                {
                    TextBox textBoxSName = (TextBox)StudentGridView.Rows[rowIndex].Cells[1].FindControl("StudentName");
                    TextBox textBoxSID = (TextBox)StudentGridView.Rows[rowIndex].Cells[2].FindControl("StudentID");
                    TextBox textBoxSAdd = (TextBox)StudentGridView.Rows[rowIndex].Cells[3].FindControl("StudentAddress");
                    TextBox textBoxSPNumber = (TextBox)StudentGridView.Rows[rowIndex].Cells[4].FindControl("StudentPNumber");
                    TextBox textBoxSEAdd = (TextBox)StudentGridView.Rows[rowIndex].Cells[5].FindControl("StudentEAddress");
                    DropDownList dropDownListSCourse = (DropDownList)StudentGridView.Rows[rowIndex].Cells[6].FindControl("StudentCourse");
                    TextBox textBoxPID = (TextBox)StudentGridView.Rows[rowIndex].Cells[7].FindControl("ParentID");

                    dataRowCurrent = dataTableCurrent.NewRow();
                    dataRowCurrent["RowNumber"] = i + 1;
                    dataTableCurrent.Rows[i - 1]["StudentName"] = textBoxSName.Text;
                    dataTableCurrent.Rows[i - 1]["StudentID"] = textBoxSID.Text;
                    dataTableCurrent.Rows[i - 1]["StudentAddress"] = textBoxSAdd.Text;
                    dataTableCurrent.Rows[i - 1]["StudentPNumber"] = textBoxSPNumber.Text;
                    dataTableCurrent.Rows[i - 1]["StudentEAddress"] = textBoxSEAdd.Text;
                    dataTableCurrent.Rows[i - 1]["StudentCourse"] = dropDownListSCourse.SelectedValue;
                    dataTableCurrent.Rows[i - 1]["ParentID"] = textBoxPID.Text;

                    rowIndex++;
                }
                ViewState["StudentGridView"] = dataTableCurrent;
            }
        }
        else
        {
            Response.Write("ViewState is null.");
        }
    }

    protected void StudentGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SetRowDataStudent();
        if (ViewState["StudentGridView"] != null)
        {
            DataTable dataTableCurrent = (DataTable)ViewState["StudentGridView"];
            DataRow dataRowCurrent = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dataTableCurrent.Rows.Count > 1)
            {
                dataTableCurrent.Rows.Remove(dataTableCurrent.Rows[rowIndex]);
                dataRowCurrent = dataTableCurrent.NewRow();
                ViewState["StudentGridView"] = dataTableCurrent;
                StudentGridView.DataSource = dataTableCurrent;
                StudentGridView.DataBind();

                for (int i = 0; i < StudentGridView.Rows.Count - 1; i++)
                {
                    StudentGridView.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
                SetPreviousDataStudent();
            }
        }
    }

    private string GetConnectionString()
    {
        return System.Configuration.ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
    }

    private string SaveAllNewStudent(StringCollection stringCollection)
    {
        SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
        StringBuilder stringBuilder = new StringBuilder(string.Empty);
        StringBuilder stringBuilder1 = new StringBuilder(string.Empty);
        StringBuilder stringBuilder2 = new StringBuilder(string.Empty);
        string[] splitItems = null;
        string[] splitItems1 = null;
        string[] splitItems2 = null;
        foreach (string item in stringCollection)
        {
            const string sqlQuery = "INSERT INTO STUDENT (NAME, ID, ADDRESS, PHONENUMBER, COURSE, PARENTID) VALUES";
            if (item.Contains("~"))
            {
                splitItems = item.Split("~".ToCharArray());
                stringBuilder.AppendFormat("{0}('{1}','{2}','{3}','{4}','{5}','{6}');", sqlQuery, splitItems[0], splitItems[1], splitItems[2], splitItems[3], splitItems[5], splitItems[7]);
            }
        }
        foreach (string item1 in stringCollection)
        {
            const string sqlQuery1 = "INSERT INTO LOGINTB (USERID, PASSWORD, MEMBERSTAT, EMAIL) VALUES";
            if (item1.Contains("~"))
            {
                splitItems1 = item1.Split("~".ToCharArray());
                stringBuilder1.AppendFormat("{0}('{1}','{2}','{3}','{4}');", sqlQuery1, splitItems1[1], splitItems1[3], 4, splitItems1[4]);
            }
        }
        foreach (string item2 in stringCollection)
        {
            const string sqlQuery1 = "INSERT INTO LOGINTB (USERID, PASSWORD, MEMBERSTAT, EMAIL) VALUES";
            if (item2.Contains("~"))
            {
                splitItems2 = item2.Split("~".ToCharArray());
                stringBuilder2.AppendFormat("{0}('{1}','{2}','{3}','{4}');", sqlQuery1, splitItems1[1], splitItems1[3], 2, splitItems1[4]);
            }
        }
        try
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(stringBuilder.ToString(), sqlConnection);
            SqlCommand sqlCommand1 = new SqlCommand(stringBuilder1.ToString(), sqlConnection);
            SqlCommand sqlCommand2 = new SqlCommand(stringBuilder2.ToString(), sqlConnection);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand1.CommandType = CommandType.Text;
            sqlCommand2.CommandType = CommandType.Text;
            sqlCommand.ExecuteNonQuery();
            sqlCommand1.ExecuteNonQuery();
            sqlCommand2.ExecuteNonQuery();
        }
        catch (System.Data.SqlClient.SqlException e)
        {
            string message = e.Message.ToString();
            //throw new Exception(message);
            //Response.Write("<script>alert('123456789')</script>");
            return "-1";
        }
        finally
        {
            sqlConnection.Close();
        }
        return "0";
    }

    protected void ButtonSaveNewStudent_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int rowIndex = 0;
            StringCollection stringCollection = new StringCollection();
            String status;
            if (ViewState["StudentGridView"] != null)
            {
                DataTable dataTableCurrent = (DataTable)ViewState["StudentGridView"];
                if (dataTableCurrent.Rows.Count > 0)
                {
                    for (int i = 1; i <= dataTableCurrent.Rows.Count; i++)
                    {
                        TextBox textBoxSName = (TextBox)StudentGridView.Rows[rowIndex].Cells[1].FindControl("StudentName");
                        TextBox textBoxSID = (TextBox)StudentGridView.Rows[rowIndex].Cells[2].FindControl("StudentID");
                        TextBox textBoxSAdd = (TextBox)StudentGridView.Rows[rowIndex].Cells[3].FindControl("StudentAddress");
                        TextBox textBoxSPNumber = (TextBox)StudentGridView.Rows[rowIndex].Cells[4].FindControl("StudentPNumber");
                        TextBox textBoxSEAdd = (TextBox)StudentGridView.Rows[rowIndex].Cells[5].FindControl("StudentEAddress");
                        DropDownList dropDownListSCourse = (DropDownList)StudentGridView.Rows[rowIndex].Cells[6].FindControl("StudentCourse");
                        TextBox textBoxPID = (TextBox)StudentGridView.Rows[rowIndex].Cells[7].FindControl("ParentID");
                        stringCollection.Add(textBoxSName.Text + "~" + textBoxSID.Text + "~" + textBoxSAdd.Text + "~" + textBoxSPNumber.Text + "~"
                            + textBoxSEAdd.Text + "~" + dropDownListSCourse.SelectedValue.ToString() + "~" + textBoxPID.Text);
                        rowIndex++;
                    }
                }
            }
            status = SaveAllNewStudent(stringCollection);
            if (status.Equals("-1"))
            {
                Response.Write("<script>alert('One or more entry already available in the database.')</script>");
            }
            else if (status.Equals("0"))
            {
                Response.Write("<script>alert('All entry for student are successfully saved.')</script>");
                DisplayStudentGridView();
            }
        }
    }

    //AddLecturer
    private void DisplayLecturerGridView()
    {
        DataTable dataTableEmpty = new DataTable();
        DataRow dataRowEmpty = null;
        dataTableEmpty.Columns.Add(new DataColumn("RowNumber"));
        dataTableEmpty.Columns.Add(new DataColumn("LecturerName"));
        dataTableEmpty.Columns.Add(new DataColumn("LecturerID"));
        dataTableEmpty.Columns.Add(new DataColumn("LecturerAddress"));
        dataTableEmpty.Columns.Add(new DataColumn("LecturerPNumber"));
        dataTableEmpty.Columns.Add(new DataColumn("LecturerEAddress"));
        dataTableEmpty.Columns.Add(new DataColumn("LecturerCourse"));

        dataRowEmpty = dataTableEmpty.NewRow();
        dataRowEmpty["RowNumber"] = 1;
        dataRowEmpty["LecturerName"] = "";
        dataRowEmpty["LecturerID"] = "";
        dataRowEmpty["LecturerAddress"] = "";
        dataRowEmpty["LecturerPNumber"] = "";
        dataRowEmpty["LecturerEAddress"] = "";
        dataRowEmpty["LecturerCourse"] = "";
        dataTableEmpty.Rows.Add(dataRowEmpty);

        ViewState["LecturerGridView"] = dataTableEmpty;
        LecturerGridView.DataSource = dataTableEmpty;
        LecturerGridView.DataBind();
    }

    protected void OnRowDataBoundLecturer(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBoxList checkBoxListLecturer = (e.Row.FindControl("LecturerCourse") as CheckBoxList);
            checkBoxListLecturer.DataSource = GetData("SELECT COURSEID + ' ' +  COURSENAME AS COURSE1 FROM COURSE;");
            checkBoxListLecturer.DataTextField = "COURSE1";
            checkBoxListLecturer.DataValueField = "COURSE1";
            checkBoxListLecturer.DataBind();

            //listBoxLecturer.Items.Insert(0, "-- Select Course --");
        }
    }

    private void SetPreviousDataLecturer()
    {
        int rowIndex = 0;

        if (ViewState["LecturerGridView"] != null)
        {
            DataTable dataTableCurrent = (DataTable)ViewState["LecturerGridView"];
            if (dataTableCurrent.Rows.Count > 0)
            {
                for (int i = 0; i < dataTableCurrent.Rows.Count; i++)
                {
                    TextBox textBoxLName = (TextBox)LecturerGridView.Rows[rowIndex].Cells[1].FindControl("LecturerName");
                    TextBox textBoxLID = (TextBox)LecturerGridView.Rows[rowIndex].Cells[2].FindControl("LecturerID");
                    TextBox textBoxLAdd = (TextBox)LecturerGridView.Rows[rowIndex].Cells[3].FindControl("LecturerAddress");
                    TextBox textBoxLPNumber = (TextBox)LecturerGridView.Rows[rowIndex].Cells[4].FindControl("LecturerPNumber");
                    TextBox textBoxLEAdd = (TextBox)LecturerGridView.Rows[rowIndex].Cells[5].FindControl("LecturerEAddress");
                    CheckBoxList checkBoxListLCourse = (CheckBoxList)LecturerGridView.Rows[rowIndex].Cells[6].FindControl("LecturerCourse");

                    LecturerGridView.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    textBoxLName.Text = dataTableCurrent.Rows[i]["LecturerName"].ToString();
                    textBoxLID.Text = dataTableCurrent.Rows[i]["LecturerID"].ToString();
                    textBoxLAdd.Text = dataTableCurrent.Rows[i]["LecturerAddress"].ToString();
                    textBoxLPNumber.Text = dataTableCurrent.Rows[i]["LecturerPNumber"].ToString();
                    textBoxLEAdd.Text = dataTableCurrent.Rows[i]["LecturerEAddress"].ToString();
                    string[] items = dataTableCurrent.Rows[i]["LecturerCourse"].ToString().Split(',');
                    for (int j = 0; j < checkBoxListLCourse.Items.Count; j++)
                    {
                        if (items.Contains(checkBoxListLCourse.Items[j].Value))
                        {
                            checkBoxListLCourse.Items[j].Selected = true;
                        }
                    }
                    rowIndex++;
                }
            }
        }
    }

    private void AddNewRowToLecturerGV()
    {
        int rowIndex = 0;

        if (ViewState["LecturerGridView"] != null)
        {
            DataTable dataTableCurrent = (DataTable)ViewState["LecturerGridView"];
            DataRow dataRowCurrent = null;
            if (dataTableCurrent.Rows.Count > 0)
            {
                for (int i = 1; i <= dataTableCurrent.Rows.Count; i++)
                {
                    TextBox textBoxLName = (TextBox)LecturerGridView.Rows[rowIndex].Cells[1].FindControl("LecturerName");
                    TextBox textBoxLID = (TextBox)LecturerGridView.Rows[rowIndex].Cells[2].FindControl("LecturerID");
                    TextBox textBoxLAdd = (TextBox)LecturerGridView.Rows[rowIndex].Cells[3].FindControl("LecturerAddress");
                    TextBox textBoxLPNumber = (TextBox)LecturerGridView.Rows[rowIndex].Cells[4].FindControl("LecturerPNumber");
                    TextBox textBoxLEAdd = (TextBox)LecturerGridView.Rows[rowIndex].Cells[5].FindControl("LecturerEAddress");
                    CheckBoxList checkBoxListLCourse = (CheckBoxList)LecturerGridView.Rows[rowIndex].Cells[6].FindControl("LecturerCourse");

                    dataRowCurrent = dataTableCurrent.NewRow();
                    dataRowCurrent["RowNumber"] = i + 1;
                    dataTableCurrent.Rows[i - 1]["LecturerName"] = textBoxLName.Text;
                    dataTableCurrent.Rows[i - 1]["LecturerID"] = textBoxLID.Text;
                    dataTableCurrent.Rows[i - 1]["LecturerAddress"] = textBoxLAdd.Text;
                    dataTableCurrent.Rows[i - 1]["LecturerPNumber"] = textBoxLPNumber.Text;
                    dataTableCurrent.Rows[i - 1]["LecturerEAddress"] = textBoxLEAdd.Text;
                    string selectedvalues = "";
                    foreach (ListItem item in checkBoxListLCourse.Items)
                    {
                        if (item.Selected)
                        {
                            selectedvalues += item.Value + ",";
                        }
                    }
                    dataTableCurrent.Rows[i - 1]["LecturerCourse"] = selectedvalues;
                    rowIndex++;
                }
                dataTableCurrent.Rows.Add(dataRowCurrent);
                ViewState["LecturerGridView"] = dataTableCurrent;

                LecturerGridView.DataSource = dataTableCurrent;
                LecturerGridView.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null.");
        }
        SetPreviousDataLecturer();
    }

    protected void ButtonAddNewLecturer_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            AddNewRowToLecturerGV();
        }
    }

    private void SetRowDataLecturer()
    {
        int rowIndex = 0;
        if (ViewState["LecturerGridView"] != null)
        {
            DataTable dataTableCurrent = (DataTable)ViewState["LecturerGridView"];
            DataRow dataRowCurrent = null;
            if (dataTableCurrent.Rows.Count > 0)
            {
                for (int i = 1; i <= dataTableCurrent.Rows.Count; i++)
                {
                    TextBox textBoxLName = (TextBox)LecturerGridView.Rows[rowIndex].Cells[1].FindControl("LecturerName");
                    TextBox textBoxLID = (TextBox)LecturerGridView.Rows[rowIndex].Cells[2].FindControl("LecturerID");
                    TextBox textBoxLAdd = (TextBox)LecturerGridView.Rows[rowIndex].Cells[3].FindControl("LecturerAddress");
                    TextBox textBoxLPNumber = (TextBox)LecturerGridView.Rows[rowIndex].Cells[4].FindControl("LecturerPNumber");
                    TextBox textBoxLEAdd = (TextBox)LecturerGridView.Rows[rowIndex].Cells[5].FindControl("LecturerEAddress");
                    CheckBoxList checkBoxListLCourse = (CheckBoxList)LecturerGridView.Rows[rowIndex].Cells[6].FindControl("LecturerCourse");

                    dataRowCurrent = dataTableCurrent.NewRow();
                    dataRowCurrent["RowNumber"] = i + 1;
                    dataTableCurrent.Rows[i - 1]["LecturerName"] = textBoxLName.Text;
                    dataTableCurrent.Rows[i - 1]["LecturerID"] = textBoxLID.Text;
                    dataTableCurrent.Rows[i - 1]["LecturerAddress"] = textBoxLAdd.Text;
                    dataTableCurrent.Rows[i - 1]["LecturerPNumber"] = textBoxLPNumber.Text;
                    dataTableCurrent.Rows[i - 1]["LecturerEAddress"] = textBoxLEAdd.Text;
                    string selectedvalues = "";

                    foreach (ListItem item in checkBoxListLCourse.Items)
                    {
                        if (item.Selected)
                        {
                            selectedvalues += item.Value + ",";
                        }
                    }

                    dataTableCurrent.Rows[i - 1]["LecturerCourse"] = selectedvalues;

                    rowIndex++;
                }
                ViewState["LecturerGridView"] = dataTableCurrent;
            }
        }
        else
        {
            Response.Write("ViewState is null.");
        }
    }

    protected void LecturerGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SetRowDataLecturer();
        if (ViewState["LecturerGridView"] != null)
        {
            DataTable dataTableCurrent = (DataTable)ViewState["LecturerGridView"];
            DataRow dataRowCurrent = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dataTableCurrent.Rows.Count > 1)
            {
                dataTableCurrent.Rows.Remove(dataTableCurrent.Rows[rowIndex]);
                dataRowCurrent = dataTableCurrent.NewRow();
                ViewState["LecturerGridView"] = dataTableCurrent;
                LecturerGridView.DataSource = dataTableCurrent;
                LecturerGridView.DataBind();

                for (int i = 0; i < LecturerGridView.Rows.Count - 1; i++)
                {
                    LecturerGridView.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
                SetPreviousDataLecturer();
            }
        }
    }

    protected void ButtonSaveNewLecturer_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int rowIndex = 0;
            string k = "";
            string status = "";
            if (ViewState["LecturerGridView"] != null)
            {
                DataTable dataTableCurrent = (DataTable)ViewState["LecturerGridView"];
                if (dataTableCurrent.Rows.Count > 0)
                {
                    for (int i = 1; i <= dataTableCurrent.Rows.Count; i++)
                    {
                        TextBox textBoxLName = (TextBox)LecturerGridView.Rows[rowIndex].Cells[1].FindControl("LecturerName");
                        TextBox textBoxLID = (TextBox)LecturerGridView.Rows[rowIndex].Cells[2].FindControl("LecturerID");
                        TextBox textBoxLAdd = (TextBox)LecturerGridView.Rows[rowIndex].Cells[3].FindControl("LecturerAddress");
                        TextBox textBoxLPNumber = (TextBox)LecturerGridView.Rows[rowIndex].Cells[4].FindControl("LecturerPNumber");
                        TextBox textBoxLEAdd = (TextBox)LecturerGridView.Rows[rowIndex].Cells[5].FindControl("LecturerEAddress");
                        CheckBoxList checkBoxListLCourse = (CheckBoxList)LecturerGridView.Rows[rowIndex].Cells[6].FindControl("LecturerCourse");
                        for (int j = 0; j < checkBoxListLCourse.Items.Count; j++)
                        {
                            if (checkBoxListLCourse.Items[j].Selected)
                            {
                                k = k + checkBoxListLCourse.Items[j].Text + ", ";
                            }
                        }

                        SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
                        StringBuilder stringBuilder = new StringBuilder(string.Empty);
                        const string sqlQuery = "INSERT INTO LECTURER(NAME, ID, ADDRESS, PHONENUMBER, COURSE) VALUES";
                        stringBuilder.AppendFormat("{0}('{1}','{2}','{3}','{4}','{5}');", sqlQuery, textBoxLName.Text, textBoxLID.Text, textBoxLAdd.Text, textBoxLPNumber.Text, k);
                        StringBuilder stringBuilder1 = new StringBuilder(string.Empty);
                        const string sqlQuery1 = "INSERT INTO LOGINTB (USERID, PASSWORD, MEMBERSTAT, EMAIL) VALUES";
                        stringBuilder1.AppendFormat("{0}('{1}','{2}','{3}','{4}');", sqlQuery1, textBoxLID.Text, textBoxLPNumber.Text, 2, textBoxLEAdd.Text);
                        try
                        {
                            sqlConnection.Open();
                            SqlCommand sqlCommand = new SqlCommand(stringBuilder.ToString(), sqlConnection);
                            SqlCommand sqlCommand1 = new SqlCommand(stringBuilder1.ToString(), sqlConnection);
                            sqlCommand.CommandType = CommandType.Text;
                            sqlCommand1.CommandType = CommandType.Text;
                            sqlCommand.ExecuteNonQuery();
                            sqlCommand1.ExecuteNonQuery();
                        }
                        catch (System.Data.SqlClient.SqlException error)
                        {
                            string message = error.Message.ToString();
                            //throw new Exception(message);
                            //Response.Write("<script>alert('123456789')</script>");
                            status = "1";
                        }
                        finally
                        {
                            sqlConnection.Close();

                        }
                        rowIndex++;
                    }
                    if (status.Equals("1"))
                    {
                        Response.Write("<script>alert('One or more entry already available in the database.')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('All entry for lecturer are successfully saved.')</script>");
                        DisplayLecturerGridView();
                    }
                }
            }
        }
    }

    //AddSubject
    private void DisplaySubjectGridView()
    {
        DataTable dataTableEmpty = new DataTable();
        DataRow dataRowEmpty = null;
        dataTableEmpty.Columns.Add(new DataColumn("RowNumber"));
        dataTableEmpty.Columns.Add(new DataColumn("SubjectName"));
        dataTableEmpty.Columns.Add(new DataColumn("SubjectID"));
        dataTableEmpty.Columns.Add(new DataColumn("SubjectCreditHour"));
        dataTableEmpty.Columns.Add(new DataColumn("SubjectCourse"));

        dataRowEmpty = dataTableEmpty.NewRow();
        dataRowEmpty["RowNumber"] = 1;
        dataRowEmpty["SubjectName"] = "";
        dataRowEmpty["SubjectID"] = "";
        dataRowEmpty["SubjectCreditHour"] = "";
        dataRowEmpty["SubjectCourse"] = "";
        dataTableEmpty.Rows.Add(dataRowEmpty);

        ViewState["SubjectGridView"] = dataTableEmpty;
        SubjectGridView.DataSource = dataTableEmpty;
        SubjectGridView.DataBind();
    }

    protected void OnRowDataBoundSubject(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList dropDownListSubject = (e.Row.FindControl("SubjectCourse") as DropDownList);
            dropDownListSubject.DataSource = GetData("SELECT COURSEID + ' ' + COURSENAME AS COURSE1 FROM COURSE;");
            dropDownListSubject.DataTextField = "COURSE1";
            dropDownListSubject.DataValueField = "COURSE1";
            dropDownListSubject.DataBind();

            dropDownListSubject.Items.Insert(0, "-- Select Course --");
        }
    }

    private void SetPreviousDataSubject()
    {
        int rowIndex = 0;
        if (ViewState["SubjectGridView"] != null)
        {
            DataTable dataTableCurrent = (DataTable)ViewState["SubjectGridView"];
            if (dataTableCurrent.Rows.Count > 0)
            {
                for (int i = 0; i < dataTableCurrent.Rows.Count; i++)
                {
                    TextBox textBoxSubName = (TextBox)SubjectGridView.Rows[rowIndex].Cells[1].FindControl("SubjectName");
                    TextBox textBoxSubID = (TextBox)SubjectGridView.Rows[rowIndex].Cells[2].FindControl("SubjectID");
                    TextBox textBoxSubCHour = (TextBox)SubjectGridView.Rows[rowIndex].Cells[3].FindControl("SubjectCreditHour");
                    DropDownList dropDownListSubCourse = (DropDownList)SubjectGridView.Rows[rowIndex].Cells[4].FindControl("SubjectCourse");

                    SubjectGridView.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    textBoxSubName.Text = dataTableCurrent.Rows[i]["SubjectName"].ToString();
                    textBoxSubID.Text = dataTableCurrent.Rows[i]["SubjectID"].ToString();
                    textBoxSubCHour.Text = dataTableCurrent.Rows[i]["SubjectCreditHour"].ToString();
                    dropDownListSubCourse.SelectedValue = dataTableCurrent.Rows[i]["SubjectCourse"].ToString();
                    rowIndex++;
                }
            }
        }
    }

    private void AddNewRowToSubjectGV()
    {
        int rowIndex = 0;
        if (ViewState["SubjectGridView"] != null)
        {
            DataTable dataTableCurrent = (DataTable)ViewState["SubjectGridView"];
            DataRow dataRowCurrent = null;
            if (dataTableCurrent.Rows.Count > 0)
            {
                for (int i = 1; i <= dataTableCurrent.Rows.Count; i++)
                {
                    TextBox textBoxSubName = (TextBox)SubjectGridView.Rows[rowIndex].Cells[1].FindControl("SubjectName");
                    TextBox textBoxSubID = (TextBox)SubjectGridView.Rows[rowIndex].Cells[2].FindControl("SubjectID");
                    TextBox textBoxSubCHour = (TextBox)SubjectGridView.Rows[rowIndex].Cells[3].FindControl("SubjectCreditHour");
                    DropDownList dropDownListSubCourse = (DropDownList)SubjectGridView.Rows[rowIndex].Cells[4].FindControl("SubjectCourse");

                    dataRowCurrent = dataTableCurrent.NewRow();
                    dataRowCurrent["RowNumber"] = i + 1;
                    dataTableCurrent.Rows[i - 1]["SubjectName"] = textBoxSubName.Text;
                    dataTableCurrent.Rows[i - 1]["SubjectID"] = textBoxSubID.Text;
                    dataTableCurrent.Rows[i - 1]["SubjectCreditHour"] = textBoxSubCHour.Text;
                    dataTableCurrent.Rows[i - 1]["SubjectCourse"] = dropDownListSubCourse.SelectedValue;

                    rowIndex++;
                }

                dataTableCurrent.Rows.Add(dataRowCurrent);
                ViewState["SubjectGridView"] = dataTableCurrent;

                SubjectGridView.DataSource = dataTableCurrent;
                SubjectGridView.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null.");
        }
        SetPreviousDataSubject();
    }

    protected void ButtonAddNewSubject_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            AddNewRowToSubjectGV();
        }
    }

    private void SetRowDataSubject()
    {
        int rowIndex = 0;
        if (ViewState["SubjectGridView"] != null)
        {
            DataTable dataTableCurrent = (DataTable)ViewState["SubjectGridView"];
            DataRow dataRowCurrent = null;
            if (dataTableCurrent.Rows.Count > 0)
            {
                for (int i = 1; i <= dataTableCurrent.Rows.Count; i++)
                {
                    TextBox textBoxSubName = (TextBox)SubjectGridView.Rows[rowIndex].Cells[1].FindControl("SubjectName");
                    TextBox textBoxSubID = (TextBox)SubjectGridView.Rows[rowIndex].Cells[2].FindControl("SubjectID");
                    TextBox textBoxSubCHour = (TextBox)SubjectGridView.Rows[rowIndex].Cells[3].FindControl("SubjectCreditHour");
                    DropDownList dropDownListSubCourse = (DropDownList)SubjectGridView.Rows[rowIndex].Cells[4].FindControl("SubjectCourse");

                    dataRowCurrent = dataTableCurrent.NewRow();
                    dataRowCurrent["RowNumber"] = i + 1;
                    dataTableCurrent.Rows[i - 1]["SubjectName"] = textBoxSubName.Text;
                    dataTableCurrent.Rows[i - 1]["SubjectID"] = textBoxSubID.Text;
                    dataTableCurrent.Rows[i - 1]["SubjectCreditHour"] = textBoxSubCHour.Text;
                    dataTableCurrent.Rows[i - 1]["SubjectCourse"] = dropDownListSubCourse.SelectedValue;

                    rowIndex++;
                }

                ViewState["SubjectGridView"] = dataTableCurrent;
            }
        }
        else
        {
            Response.Write("ViewState is null.");
        }
    }

    protected void SubjectGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SetRowDataSubject();
        if (ViewState["SubjectGridView"] != null)
        {
            DataTable dataTableCurrent = (DataTable)ViewState["SubjectGridView"];
            DataRow dataRowCurrent = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dataTableCurrent.Rows.Count > 1)
            {
                dataTableCurrent.Rows.Remove(dataTableCurrent.Rows[rowIndex]);
                dataRowCurrent = dataTableCurrent.NewRow();
                ViewState["SubjectGridView"] = dataTableCurrent;
                SubjectGridView.DataSource = dataTableCurrent;
                SubjectGridView.DataBind();

                for (int i = 0; i < SubjectGridView.Rows.Count - 1; i++)
                {
                    SubjectGridView.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
                SetPreviousDataSubject();
            }
        }
    }

    private string SaveAllNewSubject(StringCollection stringCollection)
    {
        SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
        StringBuilder stringBuilder = new StringBuilder(string.Empty);
        string[] splitItems = null;
        foreach (string item in stringCollection)
        {
            const string sqlQuery = "INSERT INTO SUBJECT (NAME, ID, CREDITHOUR, COURSE) VALUES";
            if (item.Contains("~"))
            {
                splitItems = item.Split("~".ToCharArray());
                stringBuilder.AppendFormat("{0}('{1}','{2}','{3}','{4}');", sqlQuery, splitItems[0], splitItems[1], splitItems[2], splitItems[3]);
            }
        }
        try
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(stringBuilder.ToString(), sqlConnection);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.ExecuteNonQuery();
        }
        catch (System.Data.SqlClient.SqlException e)
        {
            string message = e.Message.ToString();
            //throw new Exception(message);
            //Response.Write("<script>alert('123456789')</script>");
            return "-1";
        }
        finally
        {
            sqlConnection.Close();
        }
        return "0";
    }

    protected void ButtonSaveNewSubject_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int rowIndex = 0;
            StringCollection stringCollection = new StringCollection();
            String status;
            if (ViewState["SubjectGridView"] != null)
            {
                DataTable dataTableCurrent = (DataTable)ViewState["SubjectGridView"];
                if (dataTableCurrent.Rows.Count > 0)
                {
                    for (int i = 1; i <= dataTableCurrent.Rows.Count; i++)
                    {
                        TextBox textBoxSubName = (TextBox)SubjectGridView.Rows[rowIndex].Cells[1].FindControl("SubjectName");
                        TextBox textBoxSubID = (TextBox)SubjectGridView.Rows[rowIndex].Cells[2].FindControl("SubjectID");
                        TextBox textBoxSubCHour = (TextBox)SubjectGridView.Rows[rowIndex].Cells[3].FindControl("SubjectCreditHour");
                        DropDownList dropDownListLCourse = (DropDownList)SubjectGridView.Rows[rowIndex].Cells[4].FindControl("SubjectCourse");
                        stringCollection.Add(textBoxSubName.Text + "~" + textBoxSubID.Text + "~" + textBoxSubCHour.Text + "~" + dropDownListLCourse.SelectedValue.ToString());
                        rowIndex++;
                    }
                }
            }
            status = SaveAllNewSubject(stringCollection);
            if (status.Equals("-1"))
            {
                Response.Write("<script>alert('One or more entry already available in the database.')</script>");
            }
            else if (status.Equals("0"))
            {
                Response.Write("<script>alert('All entry for subject are successfully saved.')</script>");
                DisplaySubjectGridView();
            }
        }
    }

    //Add Lecturer Subject
    protected void ButtonCheckLecturerID_Click(object sender, EventArgs e)
    {
        string conStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
        SqlConnection sqlConnection = new SqlConnection(conStr);
        SqlDataReader sqlDataReader;
        SqlCommand sqlCommand;
        string query = "SELECT * FROM LECTURER WHERE ID='" + LecturerIDA1.Text.Trim() + "'";
        string lecturerCourse = "";
        string gridViewLecturerSubject = "";
        string gridViewLecturerSubject1 = "";
        string gridViewLecturerSubject2 = "";
        string gridViewLecturerSubject3 = "";
        string gridViewLecturerSubject4 = "";
        try
        {
            sqlConnection.Open();
            sqlCommand = new SqlCommand(query, sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.Read())
            {
                lecturerCourse = sqlDataReader["COURSE"].ToString();
            }
            sqlDataReader.Close();
            sqlConnection.Close();
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string message = ex.Message.ToString();
        }

        string[] courseArray = new string[ConfigurationManager.ConnectionStrings.Count];
        char[] delimiterChars = { ',' };
        courseArray = lecturerCourse.Split(delimiterChars);
        LecturerCourseA.Text += "Course: " + "\n";
        for (int i = 0; i < courseArray.Length; i++)
        {
            LecturerCourseA.Text += courseArray[i] + "\n";
        }
        if (courseArray.Length == 1)
        {
            gridViewLecturerSubject = courseArray[0];
            //gridViewLecturerSubject = "AUPAS   American University Program - Actuarial Science";
            ShowData(gridViewLecturerSubject);
        }
        else if (courseArray.Length == 2)
        {
            gridViewLecturerSubject = courseArray[0];
            //gridViewLecturerSubject = "AUPAS   American University Program - Actuarial Science";
            ShowData(gridViewLecturerSubject);
            gridViewLecturerSubject1 = courseArray[1];
            ShowData1(gridViewLecturerSubject1);
        }
        else if (courseArray.Length == 3)
        {
            gridViewLecturerSubject = courseArray[0];
            //gridViewLecturerSubject = "AUPAS   American University Program - Actuarial Science";
            ShowData(gridViewLecturerSubject);
            gridViewLecturerSubject1 = courseArray[1];
            ShowData1(gridViewLecturerSubject1);
            gridViewLecturerSubject2 = courseArray[2];
            ShowData2(gridViewLecturerSubject2);
        }
        else if (courseArray.Length == 4)
        {
            gridViewLecturerSubject = courseArray[0];
            //gridViewLecturerSubject = "AUPAS   American University Program - Actuarial Science";
            ShowData(gridViewLecturerSubject);
            gridViewLecturerSubject1 = courseArray[1];
            ShowData1(gridViewLecturerSubject1);
            gridViewLecturerSubject2 = courseArray[2];
            ShowData2(gridViewLecturerSubject2);
            gridViewLecturerSubject3 = courseArray[3];
            ShowData3(gridViewLecturerSubject3);
        }
        else
        {
            gridViewLecturerSubject = courseArray[0];
            //gridViewLecturerSubject = "AUPAS   American University Program - Actuarial Science";
            ShowData(gridViewLecturerSubject);
            gridViewLecturerSubject1 = courseArray[1];
            ShowData1(gridViewLecturerSubject1);
            gridViewLecturerSubject2 = courseArray[2];
            ShowData2(gridViewLecturerSubject2);
            gridViewLecturerSubject3 = courseArray[3];
            ShowData3(gridViewLecturerSubject3);
            gridViewLecturerSubject4 = courseArray[4];
            ShowData4(gridViewLecturerSubject4);
        }
        if (LecturerSubjectA.Items.Count > 0 || LecturerSubjectA1.Items.Count > 0 || LecturerSubjectA2.Items.Count > 0 || LecturerSubjectA3.Items.Count > 0 || LecturerSubjectA4.Items.Count > 0)
        {
            ButtonSaveLecturerSubject.Visible = true;
        }
    }

    private void ShowData(string gridViewLecturerSubject)
    {
        string query = "SELECT * FROM SUBJECT WHERE COURSE ='" + gridViewLecturerSubject + "';";

        LecturerSubjectA.DataSource = GetData(query);
        LecturerSubjectA.DataTextField = "ID";
        LecturerSubjectA.DataValueField = "ID";
        LecturerSubjectA.DataBind();
    }

    private void ShowData1(string gridViewLecturerSubject)
    {
        string query = "SELECT * FROM SUBJECT WHERE COURSE ='" + gridViewLecturerSubject + "';";

        LecturerSubjectA1.DataSource = GetData(query);
        LecturerSubjectA1.DataTextField = "ID";
        LecturerSubjectA1.DataValueField = "ID";
        LecturerSubjectA1.DataBind();
    }

    private void ShowData2(string gridViewLecturerSubject)
    {
        string query = "SELECT * FROM SUBJECT WHERE COURSE ='" + gridViewLecturerSubject + "';";

        LecturerSubjectA2.DataSource = GetData(query);
        LecturerSubjectA2.DataTextField = "ID";
        LecturerSubjectA2.DataValueField = "ID";
        LecturerSubjectA2.DataBind();
    }

    private void ShowData3(string gridViewLecturerSubject)
    {
        string query = "SELECT * FROM SUBJECT WHERE COURSE ='" + gridViewLecturerSubject + "';";

        LecturerSubjectA3.DataSource = GetData(query);
        LecturerSubjectA3.DataTextField = "ID";
        LecturerSubjectA3.DataValueField = "ID";
        LecturerSubjectA3.DataBind();
    }

    private void ShowData4(string gridViewLecturerSubject)
    {
        string query = "SELECT * FROM SUBJECT WHERE COURSE ='" + gridViewLecturerSubject + "';";

        LecturerSubjectA4.DataSource = GetData(query);
        LecturerSubjectA4.DataTextField = "ID";
        LecturerSubjectA4.DataValueField = "ID";
        LecturerSubjectA4.DataBind();
    }

    protected void ButtonSaveLecturerSubject_Click(object sender, EventArgs e)
    {
        string query = "UPDATE LECTURER SET SUBJECT = @SUBJECT WHERE ID = @ID;";
        List<String> YrStrList = new List<string>();
        foreach (ListItem item in LecturerSubjectA.Items)
        {
            if (item.Selected)
            {
                YrStrList.Add(item.Value);
            }
        }
        foreach (ListItem item in LecturerSubjectA1.Items)
        {
            if (item.Selected)
            {
                YrStrList.Add(item.Value);
            }
        }
        foreach (ListItem item in LecturerSubjectA2.Items)
        {
            if (item.Selected)
            {
                YrStrList.Add(item.Value);
            }
        }
        foreach (ListItem item in LecturerSubjectA3.Items)
        {
            if (item.Selected)
            {
                YrStrList.Add(item.Value);
            }
        }
        foreach (ListItem item in LecturerSubjectA4.Items)
        {
            if (item.Selected)
            {
                YrStrList.Add(item.Value);
            }
        }
        String YrStr = String.Join(",", YrStrList.ToArray());
        using (SqlConnection sqlConnection = new SqlConnection())
        {
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandText = query;
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();

                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@SUBJECT", YrStr);
                sqlCommand.Parameters.AddWithValue("@ID", LecturerIDA1.Text.Trim());
                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();
            }
        }
        ButtonSaveLecturerSubject.Visible = false;
    }

    //RemoveEnrollment
    protected void ButtonCheckStudentID_Click(object sender, EventArgs e)
    {
        string query = "SELECT * FROM RESULT WHERE ID='" + StudentIDA1.Text.Trim() + "'";
        StudentSubjectA.DataSource = GetData(query);
        StudentSubjectA.DataTextField = "SUBJECT";
        StudentSubjectA.DataValueField = "SUBJECT";
        StudentSubjectA.DataBind();
        if(StudentSubjectA.Items.Count > 0)
        {
            StudentSubjectA.Visible = true;
            ButtonDeleteStudentSubjectEnrollment.Visible = true;
        }
    }

    protected void ButtonDeleteStudentSubjectEnrollment_Click(object sender, EventArgs e)
    {
        string conStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
        SqlConnection sqlConnection = new SqlConnection(conStr);
        SqlCommand sqlCommand;
        string query = "DELETE FROM RESULT WHERE ID='" + StudentIDA1.Text.Trim() + "' AND SUBJECT='" + StudentSubjectA.SelectedValue.ToString() + "'";
        try
        {
            sqlConnection.Open();
            sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            Response.Write("<script>alert('Successfully delete this subject enrollment.')</script>");
            StudentSubjectA.Items.Clear();
            StudentSubjectA.Visible = false;
            ButtonDeleteStudentSubjectEnrollment.Visible = false;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string message = ex.Message.ToString();
        }
    }

    //DeleteStudent
    protected void CategoryChange(object sender, EventArgs e)
    {
        if (Category.SelectedValue.Equals("0"))
        {
            CategoryID.Text = "Student ID: ";
            ButtonCheckCategoryID.Text = "Check Student ID";
            CategoryIDStatus.Text = "Student Status";
            ButtonDeleteCategory.Text = "Yes. Delete this student.";
        }
        else if (Category.SelectedValue.Equals("1"))
        {
            CategoryID.Text = "Lecturer ID: ";
            ButtonCheckCategoryID.Text = "Check Lecturer ID";
            CategoryIDStatus.Text = "Lecturer Status";
            ButtonDeleteCategory.Text = "Yes. Delete this lecturer.";
        }
        else
        {
            CategoryID.Text = "Subject ID: ";
            ButtonCheckCategoryID.Text = "Check Subject ID";
            CategoryIDStatus.Text = "Subject Status";
            ButtonDeleteCategory.Text = "Yes. Delete this subject.";
        }
    }

    protected void ButtonCheckCategoryID_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            Category.Enabled = false;
            CategoryID1.Enabled = false;
            ButtonCheckCategoryID.Enabled = false;
            if (Category.SelectedValue.Equals("0"))
            {
                string conStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                SqlConnection sqlConnection = new SqlConnection(conStr);
                SqlDataReader sqlDataReader;
                SqlCommand sqlCommand;
                string query = "SELECT * FROM STUDENT WHERE ID='" + CategoryID1.Text.Trim() + "'";
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.Read())
                    {
                        CheckCategoryIDStatus.Text += "Student ID: " + sqlDataReader["ID"].ToString() + "\n";
                        CheckCategoryIDStatus.Text += "Student Name: " + sqlDataReader["NAME"].ToString() + "\n";
                        CheckCategoryIDStatus.Text += "Student Phone Number: " + sqlDataReader["PHONENUMBER"].ToString() + "\n";
                        CheckCategoryIDStatus.Text += "Student Address: " + sqlDataReader["ADDRESS"].ToString() + "\n";
                        CheckCategoryIDStatus.Text += "Student Course: " + sqlDataReader["COURSE"].ToString() + "\n";
                        CheckCategoryIDStatus.Text += "Do you want to delete this student?";
                        ButtonDeleteCategory.Visible = true;
                        ButtonDeleteCategory1.Visible = true;
                    }
                    else
                    {
                        CheckCategoryIDStatus.Text += "This student do not available on database.";
                        ButtonDeleteCategory2.Visible = true;
                    }
                    sqlDataReader.Close();
                    sqlConnection.Close();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    string message = ex.Message.ToString();
                }
            }
            else if (Category.SelectedValue.Equals("1"))
            {
                string conStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                SqlConnection sqlConnection = new SqlConnection(conStr);
                SqlDataReader sqlDataReader;
                SqlCommand sqlCommand;
                string query = "SELECT * FROM LECTURER WHERE ID='" + CategoryID1.Text.Trim() + "'";
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.Read())
                    {
                        CheckCategoryIDStatus.Text += "Lecturer ID: " + sqlDataReader["ID"].ToString() + "\n";
                        CheckCategoryIDStatus.Text += "Lecturer Name: " + sqlDataReader["NAME"].ToString() + "\n";
                        CheckCategoryIDStatus.Text += "Lecturer Phone Number: " + sqlDataReader["PHONENUMBER"].ToString() + "\n";
                        CheckCategoryIDStatus.Text += "LecturerAddress: " + sqlDataReader["ADDRESS"].ToString() + "\n";
                        CheckCategoryIDStatus.Text += "Lecturer Course: " + sqlDataReader["COURSE"].ToString() + "\n";
                        CheckCategoryIDStatus.Text += "Do you want to delete this lecturer?";
                        ButtonDeleteCategory.Visible = true;
                        ButtonDeleteCategory1.Visible = true;
                    }
                    else
                    {
                        CheckCategoryIDStatus.Text += "This lecturer do not available on database.";
                        ButtonDeleteCategory2.Visible = true;
                    }
                    sqlDataReader.Close();
                    sqlConnection.Close();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    string message = ex.Message.ToString();
                }
            }
            else
            {
                string conStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                SqlConnection sqlConnection = new SqlConnection(conStr);
                SqlDataReader sqlDataReader;
                SqlCommand sqlCommand;
                string query = "SELECT * FROM SUBJECT WHERE ID='" + CategoryID1.Text.Trim() + "'";
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.Read())
                    {
                        CheckCategoryIDStatus.Text += "Subject ID: " + sqlDataReader["ID"].ToString() + "\n";
                        CheckCategoryIDStatus.Text += "Subject Name: " + sqlDataReader["NAME"].ToString() + "\n";
                        CheckCategoryIDStatus.Text += "Subject Course: " + sqlDataReader["COURSE"].ToString() + "\n";
                        CheckCategoryIDStatus.Text += "Subject Credit Hour: " + sqlDataReader["CREDITHOUR"].ToString() + "\n";
                        CheckCategoryIDStatus.Text += "Do you want to delete this subject?";
                        ButtonDeleteCategory.Visible = true;
                        ButtonDeleteCategory1.Visible = true;
                    }
                    else
                    {
                        CheckCategoryIDStatus.Text += "This subject do not available on database.";
                        ButtonDeleteCategory2.Visible = true;
                    }
                    sqlDataReader.Close();
                    sqlConnection.Close();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    string message = ex.Message.ToString();
                }
            }
        }
    }

    protected void ButtonDeleteCategory_Click(object sender, EventArgs e)
    {
        if (Category.SelectedValue.Equals("0"))
        {
            string conStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conStr);
            SqlCommand sqlCommand;
            SqlCommand sqlCommand1;
            string query = "DELETE FROM STUDENT WHERE ID='" + CategoryID1.Text.Trim() + "'";
            string query1 = "DELETE FROM LOGINTB WHERE USERID='" + CategoryID1.Text.Trim() + "'";
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.ExecuteNonQuery();
                sqlCommand1 = new SqlCommand(query1, sqlConnection);
                sqlCommand1.CommandType = CommandType.Text;
                sqlCommand1.ExecuteNonQuery();
                sqlConnection.Close();
                CheckCategoryIDStatus.Text = "";
                CheckCategoryIDStatus.Text += "Successfully delete this student from database.";
                ButtonDeleteCategory.Visible = false;
                ButtonDeleteCategory1.Visible = false;
                ButtonDeleteCategory2.Visible = true;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string message = ex.Message.ToString();
            }
        }
        else if (Category.SelectedValue.Equals("1"))
        {
            string conStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conStr);
            SqlCommand sqlCommand;
            SqlCommand sqlCommand1;
            string query = "DELETE FROM LECTURER WHERE ID='" + CategoryID1.Text.Trim() + "'";
            string query1 = "DELETE FROM LOGINTB WHERE USERID='" + CategoryID1.Text.Trim() + "'";
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.ExecuteNonQuery();
                sqlCommand1 = new SqlCommand(query1, sqlConnection);
                sqlCommand1.CommandType = CommandType.Text;
                sqlCommand1.ExecuteNonQuery();
                sqlConnection.Close();
                CheckCategoryIDStatus.Text = "";
                CheckCategoryIDStatus.Text += "Successfully delete this lecturer from database.";
                ButtonDeleteCategory.Visible = false;
                ButtonDeleteCategory1.Visible = false;
                ButtonDeleteCategory2.Visible = true;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string message = ex.Message.ToString();
            }
        }
        else
        {
            string conStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conStr);
            SqlCommand sqlCommand;
            string query = "DELETE FROM SUBJECT WHERE ID='" + CategoryID1.Text.Trim() + "'";
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                CheckCategoryIDStatus.Text = "";
                CheckCategoryIDStatus.Text += "Successfully delete this subject from database.";
                ButtonDeleteCategory.Visible = false;
                ButtonDeleteCategory1.Visible = false;
                ButtonDeleteCategory2.Visible = true;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string message = ex.Message.ToString();
            }
        }
    }

    protected void ButtonDeleteCategory1_Click(object sender, EventArgs e)
    {
        Category.Enabled = true;
        CategoryID1.Enabled = true;
        CategoryID1.Text = "";
        ButtonCheckCategoryID.Enabled = true;
        CheckCategoryIDStatus.Text = "";
        ButtonDeleteCategory.Visible = false;
        ButtonDeleteCategory1.Visible = false;
    }

    protected void ButtonDeleteCategory2_Click(object sender, EventArgs e)
    {
        Category.Enabled = true;
        CategoryID1.Enabled = true;
        CategoryID1.Text = "";
        ButtonCheckCategoryID.Enabled = true;
        CheckCategoryIDStatus.Text = "";
        ButtonDeleteCategory2.Visible = false;
    }
}