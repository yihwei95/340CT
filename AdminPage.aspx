<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminPage.aspx.cs" Inherits="Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Page</title>
    <style type="text/css">
        .Initial {
            display: block;
            padding: 4px 18px 4px 18px;
            float: left;
            background: url("../Picture/InitialImage.png") no-repeat right top;
            color: Black;
            font-weight: bold;
        }

            .Initial:hover {
                color: White;
                background: url("../Picture/SelectedButton.png") no-repeat right top;
            }

        .Clicked {
            float: left;
            display: block;
            background: url("../Picture/SelectedButton.png") no-repeat right top;
            padding: 4px 18px 4px 18px;
            color: Black;
            font-weight: bold;
            color: White;
        }
    </style>
</head>
<body>
    <h1>Admin Page</h1>
    <script type="text/javascript" lang="javascript">
        function CheckCheck() {
            var chkBoxCount = this.getElementsByTagName("input");
            var max = 5;
            var i = 0;
            var tot = 0;
            for (i = 0; i < chkBoxCount.length; i++) {
                if (chkBoxCount[i].checked) {
                    tot = tot + 1;
                }
            }

            if (tot > max) {
                var k = 0;
                for (i = 0; i < chkBoxCount.length; i++) {
                    if (chkBoxCount[i].checked) {
                        k++;
                        if (k > max) {
                            chkBoxCount[i].checked = false;
                            alert('Cannot pick more than ' + max + ' courses.');
                        }
                    }
                }
            }
        }
    </script>
    <form id="form1" runat="server">
        <asp:Button Text="Add Student" BorderStyle="None" ID="AddStudentTab" CssClass="Initial" runat="server" OnClick="AddStudentTab_Click" />
        <asp:Button Text="Add Lecturer" BorderStyle="None" ID="AddLecturerTab" CssClass="Initial" runat="server" OnClick="AddLecturerTab_Click" />
        <asp:Button Text="Add Subject" BorderStyle="None" ID="AddSubjectTab" CssClass="Initial" runat="server" OnClick="AddSubjectTab_Click" />
        <asp:Button Text="Lecturer Subject" BorderStyle="None" ID="AddLecturerSubjectTab" CssClass="Initial" runat="server" OnClick="AddLecturerSubjectTab_Click" />
        <asp:Button Text="Remove Enrollment" BorderStyle="None" ID="RemoveEnrollmentTab" CssClass="Initial" runat="server" OnClick="RemoveEnrollmentTab_Click" />
        <asp:Button Text="Delete" BorderStyle="None" ID="DeleteCategoryTab" CssClass="Initial" runat="server" OnClick="DeleteCategoryTab_Click" />
        <asp:MultiView ID="MainView" runat="server">

            <asp:View ID="AddStudentView" runat="server">
                <br />
                <br />
                <div>
                    <h2>Student Registration</h2>
                </div>
                <div>
                    <asp:GridView ID="StudentGridView" runat="server" AutoGenerateColumns="false" OnRowDataBound="OnRowDataBoundStudent" ShowFooter="true" OnRowDeleting="StudentGridView_RowDeleting">
                        <Columns>
                            <asp:BoundField DataField="RowNumber" HeaderText="#" />
                            <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="100">
                                <ItemTemplate>
                                    <asp:TextBox ID="StudentName" runat="server" Text='<%# Eval("StudentName") %>' />
                                    <asp:RequiredFieldValidator ID="StudentNameValidator" runat="server" ControlToValidate="StudentName" ErrorMessage="*" ValidationGroup="StudentGroup" />
                                    <asp:RegularExpressionValidator ID="StudentNameValidator1" runat="server" ControlToValidate="StudentName" ErrorMessage="Cannot contain '." ValidationGroup="StudentGroup" ValidationExpression="[^']*" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student ID" ItemStyle-Width="100">
                                <ItemTemplate>
                                    <asp:TextBox ID="StudentID" runat="server" Text='<%# Eval("StudentID") %>' />
                                    <asp:RequiredFieldValidator ID="StudentIDValidator" runat="server" ControlToValidate="StudentID" ErrorMessage="*" ValidationGroup="StudentGroup" />
                                    <asp:RegularExpressionValidator ID="StudentIDValidator1" runat="server" ControlToValidate="StudentID" ErrorMessage="Cannot contain '." ValidationGroup="StudentGroup" ValidationExpression="[^']*" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Address" ItemStyle-Width="200">
                                <ItemTemplate>
                                    <asp:TextBox ID="StudentAddress" runat="server" Text='<%# Eval("StudentAddress") %>' Width="200" Height="50px" TextMode="MultiLine" />
                                    <asp:RequiredFieldValidator ID="StudentAddressValidator" runat="server" ControlToValidate="StudentAddress" ErrorMessage="*" ValidationGroup="StudentGroup" />
                                    <asp:RegularExpressionValidator ID="StudentAddressValidator1" runat="server" ControlToValidate="StudentAddress" ErrorMessage="Cannot contain '." ValidationGroup="StudentGroup" ValidationExpression="[^']*" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Phone Number" ItemStyle-Width="100">
                                <ItemTemplate>
                                    <asp:TextBox ID="StudentPNumber" runat="server" Text='<%# Eval("StudentPNumber") %>' />
                                    <asp:RequiredFieldValidator ID="StudentPNumberValidator" runat="server" ControlToValidate="StudentPNumber" ErrorMessage="*" ValidationGroup="StudentGroup" />
                                    <asp:RegularExpressionValidator ID="StudentPNumberValidator1" runat="server" ControlToValidate="StudentPNumber" ErrorMessage="Cannot contain '." ValidationGroup="StudentGroup" ValidationExpression="[^']*" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Email Address" ItemStyle-Width="100">
                                <ItemTemplate>
                                    <asp:TextBox ID="StudentEAddress" runat="server" Text='<%# Eval("StudentEAddress") %>' />
                                    <asp:RequiredFieldValidator ID="StudentEAddressValidator" runat="server" ControlToValidate="StudentEAddress" ErrorMessage="*" ValidationGroup="StudentGroup" />
                                    <asp:RegularExpressionValidator ID="StudentEAddressValidator1" runat="server" ControlToValidate="StudentEAddress" ErrorMessage="Cannot contain '." ValidationGroup="StudentGroup" ValidationExpression="[^']*" />
                                    <asp:RegularExpressionValidator ID="StudentEAddressValidator2" runat="server" ControlToValidate="StudentEAddress" ErrorMessage="Email format incorrect." ValidationGroup="StudentGroup" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Course" ItemStyle-Width="200">
                                <ItemTemplate>
                                    <asp:DropDownList ID="StudentCourse" runat="server" Width="200" />
                                    <asp:RequiredFieldValidator ID="StudentCourseValidator" runat="server" ControlToValidate="StudentCourse" ErrorMessage="*" InitialValue="-- Select Course --" ValidationGroup="StudentGroup" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Parent ID" ItemStyle-Width="100">
                                <ItemTemplate>
                                    <asp:TextBox ID="ParentID" runat="server" Text='<%# Eval("ParentID") %>' />
                                    <asp:RequiredFieldValidator ID="ParentIDValidator" runat="server" ControlToValidate="ParentID" ErrorMessage="*" ValidationGroup="StudentGroup" />
                                    <asp:RegularExpressionValidator ID="ParentIDValidator1" runat="server" ControlToValidate="ParentID" ErrorMessage="Cannot contain '." ValidationGroup="StudentGroup" ValidationExpression="[^']*" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" ValidationGroup="StudentGroup" />
                        </Columns>
                    </asp:GridView>
                    <asp:ImageButton ID="ButtonSaveNewStudent" runat="server" OnClick="ButtonSaveNewStudent_Click" ValidationGroup="StudentGroup" ImageUrl="~/Picture/Save.png" ImageAlign="Right" Width="50px" Height="50px" />
                    <asp:ImageButton ID="ButtonAddNewStudent" runat="server" OnClick="ButtonAddNewStudent_Click" ValidationGroup="StudentGroup" ImageUrl="~/Picture/Student_Add.png" ImageAlign="Right" Width="50px" Height="50px" />
                </div>
            </asp:View>

            <asp:View ID="AddLecturerView" runat="server">
                <br />
                <br />
                <div>
                    <h2>Lecturer Registration</h2>
                </div>
                <div>
                    <asp:GridView ID="LecturerGridView" runat="server" AutoGenerateColumns="false" OnRowDataBound="OnRowDataBoundLecturer" ShowFooter="true" OnRowDeleting="LecturerGridView_RowDeleting">
                        <Columns>
                            <asp:BoundField DataField="RowNumber" HeaderText="#" />
                            <asp:TemplateField HeaderText="Lecturer Name" ItemStyle-Width="100">
                                <ItemTemplate>
                                    <asp:TextBox ID="LecturerName" runat="server" Text='<%# Eval("LecturerName") %>' />
                                    <asp:RequiredFieldValidator ID="LecturerNameValidator" runat="server" ControlToValidate="LecturerName" ErrorMessage="*" ValidationGroup="LecturerGroup" />
                                    <asp:RegularExpressionValidator ID="LecturerNameValidator1" runat="server" ControlToValidate="LecturerName" ErrorMessage="Cannot contain '." ValidationGroup="LecturerGroup" ValidationExpression="[^']*" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lecturer ID" ItemStyle-Width="100">
                                <ItemTemplate>
                                    <asp:TextBox ID="LecturerID" runat="server" Text='<%# Eval("LecturerID") %>' />
                                    <asp:RequiredFieldValidator ID="LecturerIDValidator" runat="server" ControlToValidate="LecturerID" ErrorMessage="*" ValidationGroup="LecturerGroup" />
                                    <asp:RegularExpressionValidator ID="LecturerIDValidator1" runat="server" ControlToValidate="LecturerID" ErrorMessage="Cannot contain '." ValidationGroup="LecturerGroup" ValidationExpression="[^']*" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lecturer Address" ItemStyle-Width="200">
                                <ItemTemplate>
                                    <asp:TextBox ID="LecturerAddress" runat="server" Text='<%# Eval("LecturerAddress") %>' Width="200" Height="50px" TextMode="MultiLine" />
                                    <asp:RequiredFieldValidator ID="LecturerAddressValidator" runat="server" ControlToValidate="LecturerAddress" ErrorMessage="*" ValidationGroup="LecturerGroup" />
                                    <asp:RegularExpressionValidator ID="LecturerAddressValidator1" runat="server" ControlToValidate="LecturerAddress" ErrorMessage="Cannot contain '." ValidationGroup="LecturerGroup" ValidationExpression="[^']*" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lecturer Phone Number" ItemStyle-Width="100">
                                <ItemTemplate>
                                    <asp:TextBox ID="LecturerPNumber" runat="server" Text='<%# Eval("LecturerPNumber") %>' />
                                    <asp:RequiredFieldValidator ID="LecturerPNumberValidator" runat="server" ControlToValidate="LecturerPNumber" ErrorMessage="*" ValidationGroup="LecturerGroup" />
                                    <asp:RegularExpressionValidator ID="LecturerPNumberValidator1" runat="server" ControlToValidate="LecturerPNumber" ErrorMessage="Cannot contain '." ValidationGroup="LecturerGroup" ValidationExpression="[^']*" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lecturer Email Address" ItemStyle-Width="100">
                                <ItemTemplate>
                                    <asp:TextBox ID="LecturerEAddress" runat="server" Text='<%# Eval("LecturerEAddress") %>' />
                                    <asp:RequiredFieldValidator ID="LecturerEAddressValidator" runat="server" ControlToValidate="LecturerEAddress" ErrorMessage="*" ValidationGroup="LecturerGroup" />
                                    <asp:RegularExpressionValidator ID="LecturerEAddressValidator1" runat="server" ControlToValidate="LecturerEAddress" ErrorMessage="Cannot contain '." ValidationGroup="LecturerGroup" ValidationExpression="[^']*" />
                                    <asp:RegularExpressionValidator ID="LecturerEAddressValidator2" runat="server" ControlToValidate="LecturerEAddress" ErrorMessage="Email format incorrect." ValidationGroup="LecturerGroup" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lecturer Course" ItemStyle-Width="250">
                                <ItemTemplate>
                                    <div style="width: 250px; height: 50px; overflow-y: auto">
                                        <asp:CheckBoxList ID="LecturerCourse" runat="server" Width="200" Font-Size="X-Small" onclick="javascript:CheckCheck.call(this);" RepeatLayout="OrderedList" />
                                        <asp:CustomValidator runat="server" ID="LecturerCourseValidator" ClientValidationFunction="ValidateCourseList" ErrorMessage="Please Select At Least One Course."></asp:CustomValidator>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" ValidationGroup="LecturerGroup" />
                        </Columns>
                    </asp:GridView>
                    <asp:ImageButton ID="ButtonSaveNewLecturer" runat="server" OnClick="ButtonSaveNewLecturer_Click" ValidationGroup="LecturerGroup" ImageUrl="~/Picture/Save.png" ImageAlign="Right" Width="50px" Height="50px" />
                    <asp:ImageButton ID="ButtonAddNewLecturer" runat="server" OnClick="ButtonAddNewLecturer_Click" ValidationGroup="LecturerGroup" ImageUrl="~/Picture/Add.png" ImageAlign="Right" Width="50px" Height="50px" />
                </div>
            </asp:View>

            <asp:View ID="AddSubjectView" runat="server">
                <br />
                <br />
                <div>
                    <h2>Subject Registration</h2>
                </div>
                <div>
                    <asp:GridView ID="SubjectGridView" runat="server" AutoGenerateColumns="false" OnRowDataBound="OnRowDataBoundSubject" ShowFooter="true" OnRowDeleting="SubjectGridView_RowDeleting">
                        <Columns>
                            <asp:BoundField DataField="RowNumber" HeaderText="#" />
                            <asp:TemplateField HeaderText="Subject Name" ItemStyle-Width="120">
                                <ItemTemplate>
                                    <asp:TextBox ID="SubjectName" runat="server" Text='<%# Eval("SubjectName") %>' />
                                    <asp:RequiredFieldValidator ID="SubjectNameValidator" runat="server" ControlToValidate="SubjectName" ErrorMessage="*" ValidationGroup="SubjectGroup" />
                                    <asp:RegularExpressionValidator ID="SubjectNameValidator1" runat="server" ControlToValidate="SubjectName" ErrorMessage="Cannot contain '." ValidationGroup="SubjectGroup" ValidationExpression="[^']*" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Subject ID" ItemStyle-Width="120">
                                <ItemTemplate>
                                    <asp:TextBox ID="SubjectID" runat="server" Text='<%# Eval("SubjectID") %>' />
                                    <asp:RequiredFieldValidator ID="SubjectIDValidator" runat="server" ControlToValidate="SubjectID" ErrorMessage="*" ValidationGroup="SubjectGroup" />
                                    <asp:RegularExpressionValidator ID="SubjectIDValidator1" runat="server" ControlToValidate="SubjectID" ErrorMessage="Cannot contain '." ValidationGroup="SubjectGroup" ValidationExpression="[^']*" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Subject Credit Hour" ItemStyle-Width="120">
                                <ItemTemplate>
                                    <asp:TextBox ID="SubjectCreditHour" runat="server" Text='<%# Eval("SubjectCreditHour") %>' />
                                    <asp:RequiredFieldValidator ID="SubjectCreditHourValidator" runat="server" ControlToValidate="SubjectCreditHour" ErrorMessage="*" ValidationGroup="SubjectGroup" />
                                    <asp:CompareValidator ID="SubjectCreditHourValidator1" runat="server" ControlToValidate="SubjectCreditHour" ErrorMessage="Must be number." Operator="DataTypeCheck" Type="Integer" ValidationGroup="SubjectGroup" />
                                    <asp:RegularExpressionValidator ID="SubjectCreditHourValidator2" runat="server" ControlToValidate="SubjectCreditHour" ErrorMessage="Cannot contain '." ValidationGroup="SubjectGroup" ValidationExpression="[^']*" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Subject Course" ItemStyle-Width="200">
                                <ItemTemplate>
                                    <asp:DropDownList ID="SubjectCourse" runat="server" Width="200" />
                                    <asp:RequiredFieldValidator ID="SubjectCourseValidator" runat="server" ControlToValidate="SubjectCourse" ErrorMessage="*" InitialValue="-- Select Course --" ValidationGroup="SubjectGroup" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" ValidationGroup="SubjectGroup" />
                        </Columns>
                    </asp:GridView>
                    <div align="center" style="width: 1500px">
                        <asp:ImageButton ID="ButtonAddNewSubject" runat="server" OnClick="ButtonAddNewSubject_Click" ValidationGroup="SubjectGroup" ImageUrl="~/Picture/Add.png" Style="position: relative; top: 2px" Width="50px" Height="50px" />
                        <asp:ImageButton ID="ButtonSaveNewSubject" runat="server" OnClick="ButtonSaveNewSubject_Click" ValidationGroup="SubjectGroup" ImageUrl="~/Picture/Save.png" Style="position: relative; top: 2px" Width="50px" Height="50px" />
                    </div>
                </div>
            </asp:View>

            <asp:View ID="AddLecturerSubjectView" runat="server">
                <br />
                <br />
                <div>
                    <h2>Lecturer Subject Registration</h2>
                </div>
                <asp:Label ID="LecturerIDA" Text="Lecturer ID: " runat="server" />
                <asp:TextBox ID="LecturerIDA1" runat="server" />
                <asp:Button ID="ButtonCheckLecturerID" runat="server" OnClick="ButtonCheckLecturerID_Click" Text="Check Lecturer ID" />
                <br />
                <br />
                <asp:TextBox ID="LecturerCourseA" runat="server" Height="200px" Width="500px" TextMode="MultiLine" ReadOnly="true" />
                <br />
                <br />
                <asp:CheckBoxList ID="LecturerSubjectA" runat="server" />
                <br />
                <br />
                <asp:CheckBoxList ID="LecturerSubjectA1" runat="server" />
                <br />
                <br />
                <asp:CheckBoxList ID="LecturerSubjectA2" runat="server" />
                <br />
                <br />
                <asp:CheckBoxList ID="LecturerSubjectA3" runat="server" />
                <br />
                <br />
                <asp:CheckBoxList ID="LecturerSubjectA4" runat="server" />
                <br />
                <br />
                <asp:Button ID="ButtonSaveLecturerSubject" runat="server" OnClick="ButtonSaveLecturerSubject_Click" Text="Save Lecturer Subject" Visible="false" />
            </asp:View>

            <asp:View ID="RemoveEnrollmentView" runat="server">
                <br />
                <br />
                <div>
                    <h2>Remove Enrollment</h2>
                </div>
                <asp:Label ID="StudentIDA" Text="Student ID:" runat="server" />
                <asp:TextBox ID="StudentIDA1" runat="server" />
                <asp:Button ID="ButtonCheckStudentID" runat="server" OnClick="ButtonCheckStudentID_Click" Text="Check Student ID" />
                <br />
                <br />
                <asp:DropDownList ID="StudentSubjectA" runat="server" Visible="false" />
                <asp:Button ID="ButtonDeleteStudentSubjectEnrollment" runat="server" Visible="false" OnClick="ButtonDeleteStudentSubjectEnrollment_Click" Text="Delete This Subject Enrollment" />
            </asp:View>


            <asp:View ID="DeleteCategoryView" runat="server">
                <br />
                <br />
                <div>
                    <h2>Delete</h2>
                    <asp:DropDownList ID="Category" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CategoryChange">
                        <asp:ListItem Text="Student" Value="0" />
                        <asp:ListItem Text="Lecturer" Value="1" />
                        <asp:ListItem Text="Subject" Value="2" />
                    </asp:DropDownList>
                    <asp:Label ID="CategoryID" Text="Student ID:" runat="server" />
                    <asp:TextBox ID="CategoryID1" runat="server" />
                    <asp:Button ID="ButtonCheckCategoryID" runat="server" OnClick="ButtonCheckCategoryID_Click" Text="Check Student ID" />
                    <br />
                    <br />
                    <asp:Label ID="CategoryIDStatus" Text="Student Status" runat="server" />
                    <br />
                    <br />
                    <asp:TextBox ID="CheckCategoryIDStatus" runat="server" Height="200px" Width="500px" TextMode="MultiLine" ReadOnly="true" />
                    <br />
                    <br />
                    <asp:Button ID="ButtonDeleteCategory" Text="Yes. Delete this student." runat="server" Visible="false" OnClick="ButtonDeleteCategory_Click" />
                    <asp:Button ID="ButtonDeleteCategory1" Text="No. Cancel." runat="server" Visible="false" OnClick="ButtonDeleteCategory1_Click" />
                    <br />
                    <br />
                    <asp:Button ID="ButtonDeleteCategory2" Text="Okay." runat="server" Visible="false" OnClick="ButtonDeleteCategory2_Click" />
                </div>
            </asp:View>
        </asp:MultiView>
    </form>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.0.0-beta/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.0.0-beta/js/bootstrap.min.js"></script>
</body>
</html>
