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

public partial class StudentEnrollment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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

    private void OnRowDataBoundSubject()
    {
        StudentSubjectDDL.DataSource = GetData("SELECT SUB.ID, SUB.NAME FROM SUBJECT SUB LEFT JOIN RESULT RES ON RES.SUBJECT = SUB.ID AND RES.ID = '" + StudentID1.Text.Trim() + "' WHERE SUB.COURSE = '" + StudentCourse1.Text.Trim() + "' AND RES.SUBJECT IS NULL;");
        StudentSubjectDDL.DataTextField = "NAME";
        StudentSubjectDDL.DataValueField = "ID";
        StudentSubjectDDL.DataBind();

        StudentSubjectDDL.Items.Insert(0, "-- Select Course --");
    }

    protected void AddStudentSubjectB_Click(object sender, EventArgs e)
    {
        OnRowDataBoundSubject();
        if (StudentSubjectDDL.Items.Count > 1)
        {
            AddStudentSubjectB.Enabled = false;
            StudentSubjectDDL.Visible = true;
            EnrollStudentSubjectB.Visible = true;
            CancelEnrollStudentSubjectB.Visible = true;
            AddStudentSubjectB1.Visible = true;
        }
        else
        {
            Response.Write("<script>alert('No Subject Available For Enrollment.')</script>");
        }
    }

    protected void AddStudentSubjectB1_Click(object sender, EventArgs e)
    {
        if (StudentSubjectDDL.Items.Count > 2)
        {
            foreach (ListItem item in StudentSubjectDDL.Items)
            {
                if (item.Selected)
                {
                    item.Attributes.Add("disabled", "disabled");
                }
                else
                {
                    StudentSubjectDDL1.Items.Add(item);
                }
            }
            StudentSubjectDDL.Enabled = false;
            CancelEnrollStudentSubjectB.Enabled = false;
            AddStudentSubjectB1.Enabled = false;
            StudentSubjectDDL1.Visible = true;
            CancelEnrollStudentSubjectB1.Visible = true;
            AddStudentSubjectB2.Visible = true;
        }
    }

    protected void AddStudentSubjectB2_Click(object sender, EventArgs e)
    {
        if (StudentSubjectDDL.Items.Count > 3)
        {
            foreach (ListItem item in StudentSubjectDDL1.Items)
            {
                if (item.Selected)
                {
                    item.Attributes.Add("disabled", "disabled");
                }
                else
                {
                    StudentSubjectDDL2.Items.Add(item);
                }
            }
            StudentSubjectDDL1.Enabled = false;
            CancelEnrollStudentSubjectB1.Enabled = false;
            AddStudentSubjectB2.Enabled = false;
            StudentSubjectDDL2.Visible = true;
            CancelEnrollStudentSubjectB2.Visible = true;
            AddStudentSubjectB3.Visible = true;
        }
    }

    protected void AddStudentSubjectB3_Click(object sender, EventArgs e)
    {
        if (StudentSubjectDDL.Items.Count > 4)
        {
            foreach (ListItem item in StudentSubjectDDL2.Items)
            {
                if (item.Selected)
                {
                    item.Attributes.Add("disabled", "disabled");
                }
                else
                {
                    StudentSubjectDDL3.Items.Add(item);
                }
            }
            StudentSubjectDDL2.Enabled = false;
            CancelEnrollStudentSubjectB2.Enabled = false;
            AddStudentSubjectB3.Enabled = false;
            StudentSubjectDDL3.Visible = true;
            CancelEnrollStudentSubjectB3.Visible = true;
            AddStudentSubjectB4.Visible = true;
        }
    }

    protected void AddStudentSubjectB4_Click(object sender, EventArgs e)
    {
        if (StudentSubjectDDL.Items.Count > 5)
        {
            foreach (ListItem item in StudentSubjectDDL3.Items)
            {
                if (item.Selected)
                {
                    item.Attributes.Add("disabled", "disabled");
                }
                else
                {
                    StudentSubjectDDL4.Items.Add(item);
                }
            }
            StudentSubjectDDL3.Enabled = false;
            CancelEnrollStudentSubjectB3.Enabled = false;
            AddStudentSubjectB4.Enabled = false;
            StudentSubjectDDL4.Visible = true;
            CancelEnrollStudentSubjectB4.Visible = true;
        }
    }

    private string GetConnectionString()
    {
        return System.Configuration.ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
    }

    private void SaveSubject(StringCollection stringCollection)
    {
        SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
        StringBuilder stringBuilder = new StringBuilder(string.Empty);
        string[] splitItems = null;
        foreach (string item in stringCollection)
        {
            const string sqlQuery = "INSERT INTO RESULT (ID, SUBJECT) VALUES";
            if (item.Contains("~"))
            {
                splitItems = item.Split("~".ToCharArray());
                stringBuilder.AppendFormat("{0}('{1}','{2}');", sqlQuery, splitItems[0], splitItems[1]);
            }
        }
        try
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(stringBuilder.ToString(), sqlConnection);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.ExecuteNonQuery();
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string message = ex.Message.ToString();
        }
        finally
        {
            sqlConnection.Close();
        }
    }

    protected void EnrollStudentSubjectB_Click(object sender, EventArgs e)
    {
        if (StudentSubjectDDL4.SelectedIndex > -1)
        {
            if (Page.IsValid)
            {
                StringCollection stringCollection = new StringCollection();
                StringCollection stringCollection1 = new StringCollection();
                StringCollection stringCollection2 = new StringCollection();
                StringCollection stringCollection3 = new StringCollection();
                StringCollection stringCollection4 = new StringCollection();
                stringCollection.Add(StudentID1.Text + "~" + StudentSubjectDDL.SelectedValue.ToString());
                stringCollection1.Add(StudentID1.Text + "~" + StudentSubjectDDL1.SelectedValue.ToString());
                stringCollection2.Add(StudentID1.Text + "~" + StudentSubjectDDL2.SelectedValue.ToString());
                stringCollection3.Add(StudentID1.Text + "~" + StudentSubjectDDL3.SelectedValue.ToString());
                stringCollection4.Add(StudentID1.Text + "~" + StudentSubjectDDL4.SelectedValue.ToString());
                SaveSubject(stringCollection);
                SaveSubject(stringCollection1);
                SaveSubject(stringCollection2);
                SaveSubject(stringCollection3);
                SaveSubject(stringCollection4);
                CancelB();
                CancelB1();
                CancelB2();
                CancelB3();
                CancelB4();
            }
        }
        else if (StudentSubjectDDL3.SelectedIndex > -1)
        {
            if (Page.IsValid)
            {
                StringCollection stringCollection = new StringCollection();
                StringCollection stringCollection1 = new StringCollection();
                StringCollection stringCollection2 = new StringCollection();
                StringCollection stringCollection3 = new StringCollection();
                stringCollection.Add(StudentID1.Text + "~" + StudentSubjectDDL.SelectedValue.ToString());
                stringCollection1.Add(StudentID1.Text + "~" + StudentSubjectDDL1.SelectedValue.ToString());
                stringCollection2.Add(StudentID1.Text + "~" + StudentSubjectDDL2.SelectedValue.ToString());
                stringCollection3.Add(StudentID1.Text + "~" + StudentSubjectDDL3.SelectedValue.ToString());
                SaveSubject(stringCollection);
                SaveSubject(stringCollection1);
                SaveSubject(stringCollection2);
                SaveSubject(stringCollection3);
                CancelB();
                CancelB1();
                CancelB2();
                CancelB3();
            }
        }
        else if (StudentSubjectDDL2.SelectedIndex > -1)
        {
            if (Page.IsValid)
            {
                StringCollection stringCollection = new StringCollection();
                StringCollection stringCollection1 = new StringCollection();
                StringCollection stringCollection2 = new StringCollection();
                stringCollection.Add(StudentID1.Text + "~" + StudentSubjectDDL.SelectedValue.ToString());
                stringCollection1.Add(StudentID1.Text + "~" + StudentSubjectDDL1.SelectedValue.ToString());
                stringCollection2.Add(StudentID1.Text + "~" + StudentSubjectDDL2.SelectedValue.ToString());
                SaveSubject(stringCollection);
                SaveSubject(stringCollection1);
                SaveSubject(stringCollection2);
                CancelB();
                CancelB1();
                CancelB2();
            }
        }
        else if (StudentSubjectDDL1.SelectedIndex > -1)
        {
            if (Page.IsValid)
            {
                StringCollection stringCollection = new StringCollection();
                StringCollection stringCollection1 = new StringCollection();
                stringCollection.Add(StudentID1.Text + "~" + StudentSubjectDDL.SelectedValue.ToString());
                stringCollection1.Add(StudentID1.Text + "~" + StudentSubjectDDL1.SelectedValue.ToString());
                SaveSubject(stringCollection);
                SaveSubject(stringCollection1);
                CancelB();
                CancelB1();
            }
        }
        else
        {
            if (Page.IsValid)
            {
                StringCollection stringCollection = new StringCollection();
                stringCollection.Add(StudentID1.Text + "~" + StudentSubjectDDL.SelectedValue.ToString());
                SaveSubject(stringCollection);
                CancelB();
            }
        }
    }

    protected void CancelEnrollStudentSubjectB_Click(object sender, EventArgs e)
    {
        CancelB();
    }

    protected void CancelEnrollStudentSubjectB1_Click(object sender, EventArgs e)
    {
        CancelB1();
    }

    protected void CancelEnrollStudentSubjectB2_Click(object sender, EventArgs e)
    {
        CancelB2();
    }

    protected void CancelEnrollStudentSubjectB3_Click(object sender, EventArgs e)
    {
        CancelB3();
    }

    protected void CancelEnrollStudentSubjectB4_Click(object sender, EventArgs e)
    {
        CancelB4();
    }

    private void CancelB()
    {
        AddStudentSubjectB.Enabled = true;
        StudentSubjectDDL.Visible = false;
        StudentSubjectDDL.SelectedIndex = -1;
        EnrollStudentSubjectB.Visible = false;
        CancelEnrollStudentSubjectB.Visible = false;
        AddStudentSubjectB1.Visible = false;
        StudentSubjectDDL.Items.Clear();
    }

    private void CancelB1()
    {
        StudentSubjectDDL.Enabled = true;
        EnrollStudentSubjectB.Enabled = true;
        CancelEnrollStudentSubjectB.Enabled = true;
        AddStudentSubjectB1.Enabled = true;
        StudentSubjectDDL1.Visible = false;
        StudentSubjectDDL1.SelectedIndex = -1;
        CancelEnrollStudentSubjectB1.Visible = false;
        AddStudentSubjectB2.Visible = false;
        StudentSubjectDDL1.Items.Clear();
    }

    private void CancelB2()
    {
        StudentSubjectDDL1.Enabled = true;
        CancelEnrollStudentSubjectB1.Enabled = true;
        AddStudentSubjectB2.Enabled = true;
        StudentSubjectDDL2.Visible = false;
        StudentSubjectDDL2.SelectedIndex = -1;
        CancelEnrollStudentSubjectB2.Visible = false;
        AddStudentSubjectB3.Visible = false;
        StudentSubjectDDL2.Items.Clear();
    }

    private void CancelB3()
    {
        StudentSubjectDDL2.Enabled = true;
        CancelEnrollStudentSubjectB2.Enabled = true;
        AddStudentSubjectB3.Enabled = true;
        StudentSubjectDDL3.Visible = false;
        StudentSubjectDDL3.SelectedIndex = -1;
        CancelEnrollStudentSubjectB3.Visible = false;
        AddStudentSubjectB4.Visible = false;
        StudentSubjectDDL3.Items.Clear();
    }

    private void CancelB4()
    {
        StudentSubjectDDL3.Enabled = true;
        CancelEnrollStudentSubjectB3.Enabled = true;
        AddStudentSubjectB4.Enabled = true;
        StudentSubjectDDL4.Visible = false;
        StudentSubjectDDL4.SelectedIndex = -1;
        CancelEnrollStudentSubjectB4.Visible = false;
        StudentSubjectDDL4.Items.Clear();
    }
}