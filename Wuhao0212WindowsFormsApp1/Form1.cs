using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wuhao0212WindowsFormsApp1
{
    public partial class EmployeeRecorfsForm : Form
    {
        private TreeNode tvRootNode;

        public EmployeeRecorfsForm()
        {
            InitializeComponent();
            PopulateTreeView();
            InitalizelistView();
        }

        private void statusBar1_PanelClick(object sender, StatusBarPanelClickEventArgs e)
        {

        }

        private void EmployeeRecorfsForm_Load(object sender, EventArgs e)
        {

        }
        private void PopulateTreeView()
        {
            statusBarPanel1.Tag = "Refreshing Employee Code. Please wait...";
            this.Cursor = Cursors.WaitCursor;
            treeView1.Nodes.Clear();
            tvRootNode = new TreeNode("Employee Records");
            this.Cursor = Cursors.Default;
            treeView1.Nodes.Add(tvRootNode);

            TreeNodeCollection nodeCollection = tvRootNode.Nodes;
            XmlTextReader reader = new XmlTextReader("D:\\workspace\\CASE_2021-22.2\\MyRepos\\Wuhao0212WindowsFormsApp1\\Wuhao0212WindowsFormsApp1\\EmpRec.xml");
            reader.MoveToElement();
            try
            {
                while(reader.Read())
                {
                    if(reader.HasAttributes && reader.NodeType == XmlNodeType.Element)
                    {
                        reader.MoveToElement();
                        reader.MoveToElement();

                        reader.MoveToAttribute("Id");
                        String strVal = reader.Value;

                        reader.Read();
                        reader.Read();
                        if(reader.Name == "Dept")
                        {
                            reader.Read();
                        }
                        //create a child mode
                        TreeNode EcodeNode = new TreeNode(strVal);
                        //add the node
                        nodeCollection.Add(EcodeNode);
                    }
                }
                statusBarPanel1.Text = "Click on an employee code to see their record.";
            }
            catch(XmlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }//end
        protected void InitalizelistView()
        {
            listView1.Clear();
            listView1.Columns.Add("Employee Name", 255, HorizontalAlignment.Left);
            listView1.Columns.Add("Date of Join", 70, HorizontalAlignment.Right);
            listView1.Columns.Add("Gread", 105, HorizontalAlignment.Left);
            listView1.Columns.Add("Salary", 105, HorizontalAlignment.Left);
        }
        protected void PopulateListView(TreeNode crrNode)
        {
            InitalizelistView();
            XmlTextReader listRead = new XmlTextReader("D:\\workspace\\CASE_2021-22.2\\MyRepos\\Wuhao0212WindowsFormsApp1\\Wuhao0212WindowsFormsApp1\\EmpRec.xml");
            listRead.MoveToElement();
            while (listRead.Read())
            {
                String strNodeName;
                String strNodePath;
                String name;
                String gread;
                String doj;
                String sal;
                String[] strItemsArr = new string[4];
                listRead.MoveToFirstAttribute();
                strNodeName = listRead.Value;
                strNodePath = crrNode.FullPath.Remove(0, 17);
                if(strNodePath == strNodeName)
                {
                    ListViewItem lvi;
                    listRead.MoveToNextAttribute();
                    name = listRead.Value;
                    lvi = listView1.Items.Add(listRead.Value);

                    listRead.Read();
                    listRead.Read();

                    listRead.MoveToFirstAttribute();
                    doj = listRead.Value;
                    lvi.SubItems.Add (doj);
                    
                    listRead.MoveToNextAttribute();
                    gread = listRead.Value;
                    lvi.SubItems.Add(gread);

                    listRead.MoveToNextAttribute();
                    sal = listRead.Value;
                    lvi.SubItems.Add(sal);

                    listRead.MoveToNextAttribute();
                    listRead.MoveToElement();
                    listRead.ReadString();
                }
            }
        }//end PopulateListView

        private void treeView1_AfterSelect(object sender,TreeViewEventArgs e)
        {
            TreeNode currNode = e.Node;
            if (tvRootNode == currNode)
            {
                InitalizelistView();
                statusBarPanel1.Text = "Double Click the Employee code to view individual record";
                return;
            }
            else
            {
                statusBarPanel1.Text = "Click an Employee code to view individual record";
            }
            PopulateListView(currNode);
        }
    }
}
