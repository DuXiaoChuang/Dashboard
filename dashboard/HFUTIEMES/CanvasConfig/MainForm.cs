using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using Dalssoft.DiagramNet;
using System.Data.SqlClient;
using System.Text;
using System.Xml;

namespace HFUTIEMES
{
    public partial class MainForm : Form
    {
        private const string TXTPATH = @"\Login\";
        private const string TXTPOSTFIX = ".txt";
        private const string TXTPATH2 = @"\Update\";
        public string content1;
        public string guname = "";
        public string proname = "";
        public string proname0 = "";
        public string alerttype = "";
        public string txt = @"\Log\可视化监控\";
        public string postfix = @".txt";
        public static bool changeDocumentProp = true;
        public TreeNode selectNode = new TreeNode();
        public string canvasID, canvasCode, parent = "";
        string path = Application.StartupPath + @"\画布";
        public byte [] content = null;
        bool modiFied = false;
        public bool IsShowGrid = false;//是否显示网格
        fabu3 fabu3;
        DecryptEncrypt DeEn = new DecryptEncrypt();


        public MainForm()
        {
            InitializeComponent();
        }

        #region Functions
        private void Edit_UpdateUndoRedoEnable()//撤销与重做
        {
            btnUndo.Enabled = designer1.CanUndo;
            btnRedo.Enabled = designer1.CanRedo;
        }

        private void Edit_Undo()
        {
            if (designer1.CanUndo)
                designer1.Undo();

            Edit_UpdateUndoRedoEnable();
        }

        private void Edit_Redo()
        {
            if (designer1.CanRedo)
                designer1.Redo();

            Edit_UpdateUndoRedoEnable();
        }

        private void Action_None()//删除键不可用
        {
            btnDelete.Checked = false;
        }

        private void Action_Size()
        {
            Action_None();
            if (changeDocumentProp)
            {
                designer1.Document.Action = DesignerAction.Select;
                modiFied = true;
            }
        }

        private void Action_Add(ElementType e)
        {
            Action_None();
            if (changeDocumentProp)
            {
                designer1.Document.Action = DesignerAction.Add;
                designer1.Document.ElementType = e;
                modiFied = true;
            }
        }

        private void Action_Delete()
        {
            Action_None();
            btnDelete.Checked = true;
            if (changeDocumentProp)
            {
                designer1.Document.DeleteSelectedElements();
                modiFied = true;
            }
            Action_None();
        }

        private void Action_Connect()
        {
            Action_None();
            if (changeDocumentProp)
            {
                designer1.Document.Action = DesignerAction.Connect;
                modiFied = true;
            }
        }

        private void Action_Connector(LinkType lt)
        {
            Action_None();
            switch (lt)
            {
                case LinkType.Straight:
                    break;
                case LinkType.RightAngle:
                    break;
            }
            designer1.Document.LinkType = lt;
            modiFied = true;
            Action_Connect();
        }

        private void Action_Zoom(float zoom)//放大放小
        {
            designer1.Document.Zoom = zoom;
        }

        private void File_Open()
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                designer1.Open(openFileDialog1.FileName);
            }
        }

        private void File_Save()
        {
            string fileName = path + "\\" + canvasID;

            if (Operations.CreatPath(path))
            {
                //if (System.IO.File.Exists(fileName))
                //{
                //    fileName = fileName + DateTime.Now.ToFileTimeUtc();
                //}
                if (designer1.Save(fileName))
                {
                    content = Operations.ReadFileToByteArray(fileName);
                }
            }
        }

        private void Order_BringToFront()//置于顶层
        {
            if (designer1.Document.SelectedElements.Count == 1)
            {
                designer1.Document.BringToFrontElement(designer1.Document.SelectedElements[0]);
                designer1.Refresh();
                modiFied = true;
            }
        }

        private void Order_SendToBack()//置于底层
        {
            if (designer1.Document.SelectedElements.Count == 1)
            {
                designer1.Document.SendToBackElement(designer1.Document.SelectedElements[0]);
                designer1.Refresh();
                modiFied = true;
            }
        }

        private void Order_MoveUp()//上移一层
        {
            if (designer1.Document.SelectedElements.Count == 1)
            {
                designer1.Document.MoveUpElement(designer1.Document.SelectedElements[0]);
                designer1.Refresh();
                modiFied = true;
            }
        }

        private void Order_MoveDown()//下移一层
        {
            if (designer1.Document.SelectedElements.Count == 1)
            {
                designer1.Document.MoveDownElement(designer1.Document.SelectedElements[0]);
                designer1.Refresh();
                modiFied = true;
            }
        }

        private void Clipboard_Cut()//剪切
        {
            designer1.Cut();
            modiFied = true;
        }

        private void Clipboard_Copy()//复制
        {
            designer1.Copy();
            modiFied = true;
        }

        private void Clipboard_Paste()//粘贴
        {
            designer1.Paste();
            modiFied = true;
        }

        #endregion

        #region Toolbar Events

        private void mnuTbRectangle_Click(object sender, System.EventArgs e)//toolbar做botton时，click事件
        {
            Action_Add(ElementType.CommentBox);
        }

        private void mnuTbElipse_Click(object sender, System.EventArgs e)
        {
            Action_Add(ElementType.Elipse);
        }

        private void mnuTbRectangleNode_Click(object sender, System.EventArgs e)
        {
            Action_Add(ElementType.RectangleNode);
        }

        private void mnuTbElipseNode_Click(object sender, System.EventArgs e)
        {
            Action_Add(ElementType.ElipseNode);
        }

        private void TbCommentBox_Click(object sender, System.EventArgs e)
        {
            Action_Add(ElementType.CommentBox);
        }

        private void mnuTbStraightLink_Click(object sender, System.EventArgs e)
        {
            Action_Connector(LinkType.Straight);
        }

        private void mnuTbRightAngleLink_Click(object sender, System.EventArgs e)
        {
            Action_Connector(LinkType.RightAngle);
        }

        private void ToolStripMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)//toolstrip单击事件
        {
            string btn = (string)e.ClickedItem.Tag;

            if (btn == "Open") File_Open();
            if (btn == "Save")
            {
                File_Save();
                if (UpdateFiles(canvasID, content))
                {
                    modiFied = false;
                    MessageBox.Show("保存成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    MessageBox.Show("保存失败！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (btn == "Size") Action_Size();
            if (btn == "Add")
            if (btn == "Delete") Action_Delete();
            if (btn == "Connect") Action_Connect();

            if (btn == "Undo") Edit_Undo();
            if (btn == "Redo") Edit_Redo();

            if (btn == "Front") Order_BringToFront();
            if (btn == "Back") Order_SendToBack();
            if (btn == "MoveUp") Order_MoveUp();
            if (btn == "MoveDown") Order_MoveDown();

            if (btn == "Cut") Clipboard_Cut();
            if (btn == "Copy") Clipboard_Copy();
            if (btn == "Paste") Clipboard_Paste();
            //if (btn == "Property") ShowPropertyDlg();
            if (btn == "SaveAs") File_SaveAs();
            if (btn == "SelectAll") File_SelectAll();
        }

        private void File_SelectAll()
        {
            designer1.Document.SelectAllElements();
            designer1.Invalidate();
        }

        private void File_SaveAs()
        {

            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                designer1.Save(saveFileDialog1.FileName);
            }
        }

        private void butRefresh_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            Operations.LoadCanvasTree(treeView1);
            //Operations.getAllCanvas();
        }
        #endregion

        private void Form1_Load(object sender, System.EventArgs e)
        {
            getDataConnection();
            designer1.Type = TypeOfDesigner.配置;
            LoadImageOfBtn();
            Edit_UpdateUndoRedoEnable();
            SetControls();
            Operations.DeleteFolder(path);
            designer1.Document.PropertyChanged += new EventHandler(Document_PropertyChanged);
            butRefresh_Click(sender, e);
        }

        private void Document_PropertyChanged(object sender, EventArgs e)
        {
            changeDocumentProp = false;

            Action_None();

            switch (designer1.Document.Action)
            {
                case DesignerAction.Select:
                    Action_Size();
                    break;
                case DesignerAction.Delete:
                    Action_Delete();
                    break;
                case DesignerAction.Connect:
                    Action_Connect();
                    break;
                case DesignerAction.Add:
                    Action_Add(designer1.Document.ElementType);
                    break;
            }

            Edit_UpdateUndoRedoEnable();

            changeDocumentProp = true;
        }

        private void mnuZoom_10_Click(object sender, System.EventArgs e)
        {
            Action_Zoom(0.1f);
        }

        private void mnuZoom_25_Click(object sender, System.EventArgs e)
        {
            Action_Zoom(0.25f);
        }

        private void mnuZoom_50_Click(object sender, System.EventArgs e)
        {
            Action_Zoom(0.5f);
        }

        private void mnuZoom_75_Click(object sender, System.EventArgs e)
        {
            Action_Zoom(0.75f);
        }

        private void mnuZoom_100_Click(object sender, System.EventArgs e)
        {
            Action_Zoom(1f);
        }

        private void mnuZoom_150_Click(object sender, System.EventArgs e)
        {
            Action_Zoom(1.5f);
        }

        private void mnuZoom_200_Click(object sender, System.EventArgs e)
        {
            Action_Zoom(2.0f);
        }


        #region Events Handling
        private void designer1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            AppendLog("designer1_MouseUp: {0}", e.ToString());

            propertyGrid1.SelectedObject = null;

            if (designer1.Document.SelectedElements.Count == 1)
            {
                propertyGrid1.SelectedObject = designer1.Document.SelectedElements[0];
            }
            else if (designer1.Document.SelectedElements.Count > 1)
            {
                propertyGrid1.SelectedObjects = designer1.Document.SelectedElements.GetArray();
            }
            else if (designer1.Document.SelectedElements.Count == 0)
            {
                propertyGrid1.SelectedObject = designer1.Document;
            }
        }

        private void designer1_ElementClick(object sender, Dalssoft.DiagramNet.ElementEventArgs e)
        {
            AppendLog("designer1_ElementClick: {0}", e.ToString());
        }

        private void designer1_ElementMouseDown(object sender, Dalssoft.DiagramNet.ElementMouseEventArgs e)
        {
            AppendLog("designer1_ElementMouseDown: {0}", e.ToString());
        }

        private void designer1_ElementMouseUp(object sender, Dalssoft.DiagramNet.ElementMouseEventArgs e)
        {
            AppendLog("designer1_ElementMouseUp: {0}", e.ToString());
        }

        private void designer1_ElementMoved(object sender, Dalssoft.DiagramNet.ElementEventArgs e)
        {
            AppendLog("designer1_ElementMoved: {0}", e.ToString());
        }

        private void designer1_ElementMoving(object sender, Dalssoft.DiagramNet.ElementEventArgs e)
        {
            AppendLog("designer1_ElementMoving: {0}", e.ToString());
        }

        private void designer1_ElementResized(object sender, Dalssoft.DiagramNet.ElementEventArgs e)
        {
            AppendLog("designer1_ElementResized: {0}", e.ToString());
        }

        private void designer1_ElementResizing(object sender, Dalssoft.DiagramNet.ElementEventArgs e)
        {
            AppendLog("designer1_ElementResizing: {0}", e.ToString());
        }

        private void designer1_ElementConnected(object sender, Dalssoft.DiagramNet.ElementConnectEventArgs e)
        {
            AppendLog("designer1_ElementConnected: {0}", e.ToString());
        }

        private void designer1_ElementConnecting(object sender, Dalssoft.DiagramNet.ElementConnectEventArgs e)
        {
            AppendLog("designer1_ElementConnecting: {0}", e.ToString());
        }

        private void designer1_ElementSelection(object sender, Dalssoft.DiagramNet.ElementSelectionEventArgs e)
        {
            AppendLog("designer1_ElementSelection: {0}", e.ToString());
        }

        #endregion

        private void AppendLog(string log)
        {
            AppendLog(log, "");
        }

        private void AppendLog(string log, params object[] args)
        {
            txtLog.AppendText(String.Format(log, args) + "\r\n");
        }

        private void LoadImageOfBtn()
        {
            btnSave.Image = imageList1.Images[5];
            btnCut.Image = imageList1.Images[13];
            btnCopy.Image = imageList1.Images[14];
            btnPaste.Image = imageList1.Images[15];
            btnDelete.Image = imageList1.Images[2];
            btnUndo.Image = imageList1.Images[7];
            btnRedo.Image = imageList1.Images[8];
            btnZoom.Image = imageList1.Images[16];
            btnFront.Image = imageList1.Images[9];
            btnBack.Image = imageList1.Images[10];
            btnMoveUp.Image = imageList1.Images[11];
            btnMoveDown.Image = imageList1.Images[12];
            btnPro.Image = imageList1.Images[17];
            btnTools.Image = imageList1.Images[18]; ;
            btnSaveAs.Image = imageList1.Images[19];
            btnOpen.Image = imageList1.Images[6];
            btnSelectAll.Image = imageList1.Images[0];
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            selectNode = treeView1.SelectedNode;
            if (selectNode == null)
            {
                treeView1.ContextMenuStrip = null;
                return;
            }
            canvasID = selectNode.Tag.ToString();
            canvasCode = selectNode.Name;
            SetControls();
            Operations.getLocalCanvas(canvasID, designer1);
            GetProperty();
            UpdateObjectList();
            modiFied = false;

            if (selectNode.Parent == null)
            {
                启动发布3ToolStripMenuItem.Enabled = false;
            }
            else if (selectNode.Parent.Tag.ToString() == "0")
            {
                启动发布3ToolStripMenuItem.Enabled = true ;
            }
            else 
            {
                启动发布3ToolStripMenuItem.Enabled = false;
            }
        }

        public void SetControls()
        {
            if (selectNode.Level == 0)
            {
                designer1.Enabled = false;
                treeView1.ContextMenuStrip = tvContextMenuStrip;
                //ToolStripMenu.Enabled = false;
                menuItemAddCanvas.Enabled = true;
                menuItemDeleteCanvas.Enabled = false;
                menuItemModifyCanvas.Enabled = false;
                Action_Size();
                propertyGrid1.SelectedObject = null;
            }
            else
            {
                designer1.Enabled = true;
                treeView1.ContextMenuStrip = tvContextMenuStrip;
                ToolStripMenu.Enabled = true;
                menuItemAddCanvas.Enabled = true;
                menuItemDeleteCanvas.Enabled = true;
                menuItemModifyCanvas.Enabled = true;
                propertyGrid1.SelectedObject = designer1.Document;
                if (selectNode.Level == 1)
                {
                    this.menuItemModifyCanvas.Enabled = true;
                    this.menuItemDeleteCanvas.Enabled = true;
                }
            }
        }

        public void GetProperty()
        {
            //if (FormOpen.IsFormPropertyOpen)
            //{
            propertyGrid1.SelectedObject = null;

            if (selectNode.Level != 0)
            {
                if (designer1.Document.SelectedElements.Count == 1)
                {
                    Dalssoft.DiagramNet.BaseElement el = designer1.Document.SelectedElements[0];
                    propertyGrid1.SelectedObject = el;
                    //formMain.formProperty.comboBox1.Text ;
                }
                else if (designer1.Document.SelectedElements.Count > 1)
                {
                    propertyGrid1.SelectedObjects = designer1.Document.SelectedElements.GetArray();
                }
                else if (designer1.Document.SelectedElements.Count == 0)
                {
                    designer1.Document.Action = Dalssoft.DiagramNet.DesignerAction.Select;
                    Operations.GetDocumentInfo(canvasID, designer1.Document);
                    propertyGrid1.SelectedObject = designer1.Document;
                }
                //}
            }

        }

        private void UpdateObjectList()
        {
            cmbObjects.Items.Clear();
            if (selectNode.Level != 0)
            {
                cmbObjects.Items.Add(designer1.Document.画布名称 + "  " + designer1.Document.ToString());
                if (designer1.Document.Elements.Count > 0)
                {
                    for (int i = 0; i < designer1.Document.Elements.Count; i++)
                    {
                        cmbObjects.Items.Add(designer1.Document.Elements[i].ID + "  " + designer1.Document.Elements[i].ToString());
                    }
                }
                if (designer1.Document.SelectedElements.Count == 1)
                {
                    int index = designer1.Document.Elements.IndexOf(designer1.Document.SelectedElements[0]);
                    cmbObjects.SelectedIndex = index + 1;
                }
                else if (designer1.Document.SelectedElements.Count < 1)
                {
                    if (cmbObjects.Items.Count > 0)
                    {
                        cmbObjects.SelectedIndex = 0;
                    }
                }
                else
                {
                    cmbObjects.SelectedIndex = -1;
                }
            }
        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (selectNode.Level != 0)
            {
                if (modiFied)
                {
                    DialogResult r = MessageBox.Show(this, "是否保存已更改的信息?", "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (r == DialogResult.Yes)
                    {
                        File_Save();
                        if (UpdateFiles(canvasID, content))
                        {
                            MessageBox.Show("保存成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("保存失败！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
            }
        }

        public bool UpdateFiles(string canvasID, byte[] content)
        {
            string sql = "select * from AT_CANVAS_B where canvas_ID='" + canvasID + "'";
            DataTable dt = data.DBQuery.OpenTable1(sql);
            StringBuilder strBuilder = new StringBuilder();
            if (dt.Rows.Count > 0)
            {

                strBuilder.Append("update AT_CANVAS_B ");
                strBuilder.Append("set ");
                strBuilder.Append("canvas_content=@canvasContent where canvas_ID=@canvasID");
                SqlParameter[] parameters1 = {       
                                            new SqlParameter("@canvasID", SqlDbType.VarChar,50),                                      
                                            new SqlParameter("@canvasContent", SqlDbType.VarBinary)};
                parameters1[0].Value = canvasID;
                parameters1[1].Value = content;
                data.DBQuery.ExecuteSql(strBuilder.ToString(), parameters1);
                return true;
            }
            else
            {
                MessageBox.Show("信息提示", "数据库连接异常，请重试！");
                return false;
            //    strBuilder.Append("insert into AT_CANVAS_B(canvas_ID,canvas_code,canvas_name,canvas_content,parent_canvas_ID,creater_key,creat_time,last_modifier_key,last_modified_time,monitor_type,remark) ");
            //    strBuilder.Append("values ");
            //    strBuilder.Append("(@canvasID,@canvasCode,@canvasName,@canvasContent,@parentID,@founderID,@foundTime,@lastModifierID,@lastModifiedDate,@monitorType,@memo)");
            //    SqlParameter[] parameters1 = {
            //                        new SqlParameter("@canvasID", SqlDbType.NVarChar,50),
            //                        new SqlParameter("@canvasCode", SqlDbType.NVarChar,50),
            //                        new SqlParameter("@canvasName", SqlDbType.NVarChar,1000),
            //                        new SqlParameter("@canvasContent", SqlDbType.VarBinary),
            //                        new SqlParameter("@parentID", SqlDbType.NVarChar,50),
            //                        new SqlParameter("@founderID", SqlDbType.Int,8),
            //                        new SqlParameter("@foundTime", SqlDbType.DateTime,8),
            //                        new SqlParameter("@lastModifierID", SqlDbType.Int,8),
            //                        new SqlParameter("@lastModifiedDate", SqlDbType.DateTime,8),      
            //                        new SqlParameter("@monitorType", SqlDbType.Int,8),
            //                        new SqlParameter("@memo", SqlDbType.NVarChar,2000)};
            //    parameters1[0].Value = canvasID;
            //    parameters1[1].Value = canvasCode;
            //    parameters1[2].Value = selectNode.Text;
            //    parameters1[3].Value = content;
            //    parameters1[4].Value = selectNode.Parent.Tag.ToString().Trim();
            //    parameters1[5].Value = 0;
            //    parameters1[6].Value = DateTime.Now;
            //    parameters1[7].Value = 0;
            //    parameters1[8].Value = DateTime.Now;
            //    parameters1[9].Value = 1;
            //    parameters1[10].Value = "";
            //    data.DBQuery.ExecuteSql(strBuilder.ToString(), parameters1);
            }
           
        }

        private void treeView1_Click(object sender, EventArgs e)
        {
            SetControls();
        }

        private void mnuTbAndon_Click(object sender, EventArgs e)
        {
            Action_Add(ElementType.Andon);
        }

        private void mnuTbZone_Click(object sender, EventArgs e)
        {
            Action_Add(ElementType.Zone);
        }

        private void cmbObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            designer1.Document.ClearSelection();
            if (cmbObjects.SelectedIndex > 0)
            {
                designer1.Document.SelectElement(designer1.Document.Elements[cmbObjects.SelectedIndex - 1]);
            }
            GetProperty();
            //designer1.Invalidate();
        }

        private void designer1_MouseDown(object sender, MouseEventArgs e)
        {
            UpdateObjectList();
        }

        private void menuItemAddCanvas_Click(object sender, EventArgs e)
        {
            CanvasDlg dlg = new CanvasDlg();
            parent = canvasID;
            dlg.parentID = parent;
            dlg.isModified = false;
            dlg.Text = "添加画布信息";
            dlg.ShowDialog();
            if (dlg.DialogResult == DialogResult.OK)
            {
                TreeNode newNode = new TreeNode(dlg.txtName.Text, 2, 3);
                newNode.Tag = dlg.canvasID;
                newNode.Name = dlg.canvasCode;
                selectNode.Nodes.Add(newNode);
                this.treeView1.SelectedNode = newNode;
                //BinaryComponents.SuperTree.TreeNode newNode = selectNode.ChildNodes.Add();
                //newNode.Text = dlg.txtName.Text;
                //newNode.Tag = dlg.canvasID;
                //this.treeControl1.SelectedNode = newNode;
                canvasID = dlg.canvasID;
                File_Save();
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.Append("insert into AT_CANVAS_B(canvas_ID,canvas_code,canvas_name,canvas_content,parent_canvas_ID,creater_key,creat_time,last_modifier_key,last_modified_time,monitor_type,remark,is_monitor,monitor_time,locationX,locationY,size_width,size_height) ");
                strBuilder.Append("values ");
                strBuilder.Append("(@canvasID,@canvasCode,@canvasName,@canvasContent,@parentID,@founderID,@foundTime,@lastModifierID,@lastModifiedDate,@monitorType,@memo,@is_monitor,@monitor_time,@locationX,@locationY,@size_width,@size_height)");
                SqlParameter[] parameters1 = {
                                    new SqlParameter("@canvasID", SqlDbType.NVarChar,50),
                                    new SqlParameter("@canvasCode", SqlDbType.NVarChar,50),
                                    new SqlParameter("@canvasName", SqlDbType.NVarChar,1000),
                                    new SqlParameter("@canvasContent", SqlDbType.VarBinary),
                                    //new SqlParameter("@canvasContent", SqlDbType.NVarChar,50),
                                    new SqlParameter("@parentID", SqlDbType.NVarChar,50),
                                    new SqlParameter("@founderID", SqlDbType.Int,8),
                                    new SqlParameter("@foundTime", SqlDbType.DateTime,8),
                                    new SqlParameter("@lastModifierID", SqlDbType.Int,8),
                                    new SqlParameter("@lastModifiedDate", SqlDbType.DateTime,8),      
                                    new SqlParameter("@monitorType", SqlDbType.Int,8),
                                    new SqlParameter("@memo", SqlDbType.NVarChar,2000),
                                    new SqlParameter("@is_monitor", SqlDbType.Int,8),
                                    //new SqlParameter("@is_stop", SqlDbType.Int,8),
                                    new SqlParameter("@monitor_time", SqlDbType.Int,8),
                                    new SqlParameter("@locationX", SqlDbType.Int,8),
                                    new SqlParameter("@locationY", SqlDbType.Int,8),
                                    new SqlParameter("@size_width", SqlDbType.Int,8),
                                    new SqlParameter("@size_height", SqlDbType.Int,8)};

                parameters1[0].Value = dlg.canvasID;
                parameters1[1].Value = dlg.txtCode.Text;
                parameters1[2].Value = dlg.txtName.Text;
                parameters1[3].Value = content;
                parameters1[4].Value = parent;
                parameters1[5].Value = 0;
                parameters1[6].Value = DateTime.Now;
                parameters1[7].Value = 0;
                parameters1[8].Value = DateTime.Now;
                parameters1[9].Value = dlg.typeIndex;
                parameters1[10].Value = dlg.txtMemo.Text;
                parameters1[11].Value = dlg.radiobutton;
                parameters1[12].Value = dlg.numericUpDown1.Value;

                if (dlg.TxtLocationX.Text == "")
                    parameters1[13].Value = null;
                else
                    parameters1[13].Value = int.Parse(dlg.TxtLocationX.Text);
                if (dlg.TxtLocationY.Text == "")
                    parameters1[14].Value = null;
                else
                    parameters1[14].Value = int.Parse(dlg.TxtLocationY.Text);
                if (dlg.TxtSizeL.Text == "")
                    parameters1[15].Value = null;
                else
                    parameters1[15].Value = int.Parse(dlg.TxtSizeL.Text);
                if (dlg.TxtSizeH.Text == "")
                    parameters1[16].Value = null;
                else
                    parameters1[16].Value = int.Parse(dlg.TxtSizeH.Text);

                string sqlqqq = strBuilder.ToString();
                data.DBQuery.ExecuteSql(sqlqqq, parameters1);
                GetProperty();
            }
        }

        private void menuItemModifyCanvas_Click(object sender, EventArgs e)
        {
            CanvasDlg dlg = new CanvasDlg();
            dlg.canvasID = canvasID;
            dlg.parentID = Operations.GetParentCanvasId(canvasID);
            dlg.canvasCode = canvasCode;
            dlg.isModified = true;
            dlg.ShowInTaskbar = false;
            dlg.ShowDialog();
            if (dlg.DialogResult == DialogResult.OK)
            {
                canvasCode = dlg.txtCode.Text;
                selectNode.Text = dlg.txtName.Text;
                selectNode.Name = dlg.txtCode.Text;

                GetProperty();
            }
        }

        private void menuItemDeleteCanvas_Click(object sender, EventArgs e)
        {
            if (selectNode != null)
            {
                DialogResult result = MessageBox.Show(this, "确定要删除该画布及相关信息吗?", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    DeleteCanvas(canvasID);
                    selectNode.Remove();
                }
            }
        }

        private void DeleteCanvas(string ID)
        {
            //string fileName = path + "\\" + ID;
            Operations.DeleteFolder(path);
            string sql = "select canvas_ID from AT_CANVAS_B where parent_canvas_ID='" + ID + "'";
            DataTable dt = data.DBQuery.OpenTable1(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DeleteCanvas(dt.Rows[i][0].ToString());
                }
            }
            data.DBQuery.ExceuteNonQuery("delete from AT_CANVAS_B where canvas_ID='" + ID + "'");
           
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            DialogResult dlg = MessageBox.Show("确定要退出程序吗？", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            switch (dlg)
            {
                case DialogResult.OK:
                    {
                        Operations.DeleteFolder(path);
                        e.Cancel = false;
                    }
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true ;
                    return;
            }
        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            splitContainer1.Width = 0;
        }

        private void designer1_DoubleClick(object sender, EventArgs e)
        {
            designer1_DoubleClick(designer1);
        }

        public void designer1_DoubleClick(Dalssoft.DiagramNet.Designer designer)
        {
            if (designer.Document.SelectedElements.Count != 1) return;
            Dalssoft.DiagramNet.BaseElement el = designer.Document.SelectedElements[0];
            if (el.监控类型 == MonitoredType.控制地址)
            {
                AddressItemsDlg dlg = new AddressItemsDlg();
                dlg.ShowInTaskbar = false;
                dlg.ShowDialog();
                if (dlg.ok == 1)
                {
                    el.监控对象 = dlg.ItemKey;
                    el.MoniteredObjectID = dlg.ItemKey;
                    el.MoniteredObjectCode = dlg.ItemKey;
                    el.MoniteredObjectName = dlg.ItemKey;
                }
            }
       }

        private void 星期ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Action_Add(ElementType.TextWeek);
        }

        private void timeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Action_Add(ElementType.TextTime);
        }

        private void 年月日ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Action_Add(ElementType.TextDate);
        }

        private void 生产计划ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Action_Add(ElementType.WorkOrder);
        }

        private void 生产计划表头ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Action_Add(ElementType.WorkOrderTile);
        }

        private void 画线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Action_Add(ElementType.StraightLine);
        }
        
        private void setDataBase_Click(object sender, EventArgs e)
        {
            SetData dlg = new SetData();
            dlg.ShowDialog();
        }

        private void getDataConnection()
        {
            string str = Application.StartupPath;
            string datasorucePath = str + "\\config.xml";
            if (System.IO.File.Exists(datasorucePath))
            {
                XmlReader rdr = XmlReader.Create(datasorucePath);
                ArrayList al = new ArrayList();
                while (rdr.Read())
                {
                    if (rdr.Value != "")
                    {
                        al.Add(rdr.Value.ToString());
                    }
                }
                rdr.Close();
                int count = al.Count;
                data.data.dataSource = al[5].ToString();
                data.data.dataBase = al[11].ToString();
                data.data.uid = al[7].ToString();
                try
                {
                    data.data.passWord = DeEn.Decrypto(al[9].ToString());
                    //data.data.passWord = DeEn.Encrypto("sa123");
                }
                catch (Exception ex)
                {
                    data.data.passWord = "";
                    MessageBox.Show("请重新设定数据库连接！" + ex.ToString());
                }
                data.data.ConnStr = "data source=" + al[5].ToString() + ";database=" + al[11].ToString() + ";user id=" + al[7].ToString() + ";password=" + data.data.passWord + "";
            }
            string datasorucePath1 = str + "\\config1.xml";
            if (System.IO.File.Exists(datasorucePath1))
            {
                XmlReader rdr = XmlReader.Create(datasorucePath1);
                ArrayList al = new ArrayList();
                while (rdr.Read())
                {
                    if (rdr.Value != "")
                    {
                        al.Add(rdr.Value.ToString());
                    }
                }
                rdr.Close();
                //int count = al.Count;
                string kk = al[0].ToString();
                string[] gu = kk.Split('，');
                int k = gu.Length;
                guname = gu[0];
                for (int i = 1; i < k - 1; i++)
                {
                    proname = proname + gu[i] + " '" + "or pro_name = " + "'";
                    proname0 = proname0 + gu[i] + " '" + "or a.pro_name = " + "'";
                }
                proname = proname + gu[k - 1];
                proname0 = proname0 + gu[k - 1];
                Dalssoft.DiagramNet.WorkOrder.guname = guname;
                Dalssoft.DiagramNet.xiaoxi.guname = guname;
                Dalssoft.DiagramNet.WorkOrder.proname = proname;
                Dalssoft.DiagramNet.WorkOrder.proname0 = proname0;
            }
        }
  
        private void 股ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Action_Add(ElementType.EquipState);
        }

        private void lED股名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Action_Add(ElementType.ToolStateExcel );
        }

        private void 启动发布3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fabu3 = new fabu3();
            fabu3.monitorCanvasId = canvasID;
            fabu3.alerttype = alerttype;
            string sql = "select [monitor_type],[locationX],[locationY],[size_width],[size_height] from AT_CANVAS_B where [canvas_ID] = '" + canvasID + "'";
            DataTable dt = data.DBQuery.OpenTable1(sql);
            if (dt.Rows.Count > 0)
            {
                fabu3.monitorCanvasType = int.Parse(dt.Rows[0][0].ToString());
                fabu3.canvasSize.Width = int.Parse(dt.Rows[0]["size_width"].ToString());
                fabu3.canvasSize.Height = int.Parse(dt.Rows[0]["size_height"].ToString());
                fabu3.canvasPoint.X = int.Parse(dt.Rows[0]["locationX"].ToString());
                fabu3.canvasPoint.Y = int.Parse(dt.Rows[0]["locationY"].ToString());
            }
            fabu3.Show();
            启动发布3ToolStripMenuItem.Enabled = false;
            停止监控发布3ToolStripMenuItem.Enabled = true ;

        }

        private void 停止监控发布3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fabu3.Close();
            启动发布3ToolStripMenuItem.Enabled = true;
            停止监控发布3ToolStripMenuItem.Enabled = false;
        }

        private void 工位模型ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Action_Add(ElementType.OnlineNumber  );
        }

        private void 起始点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Action_Add(ElementType.StartEndPoint );
        }

        private void 结构一股生产计划ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Action_Add(ElementType.EquipAlarmExcel);
        }

        private void 产品跟踪信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Action_Add(ElementType.ProductTraceExcel);
        }

        private void 温度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Action_Add(ElementType.Temperature);
        }

        private void 湿度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Action_Add(ElementType.shidu);
        }

        private void 电能ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Action_Add(ElementType.dianneng);
        }

        private void aNDONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Action_Add(ElementType.Andon);
        }

        private void 刀具状态ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Action_Add(ElementType.ToolState );
        }
        private void 刀槽ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Action_Add(ElementType.Groove);
        }

     }
        
}

