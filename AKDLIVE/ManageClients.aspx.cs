using Syncfusion.JavaScript.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using db = DBConnections.DDF;

public partial class ManageClients : System.Web.UI.Page
{
    private List<Orders> order = new List<Orders>();

    private db.ClientsDDF _clients = new db.ClientsDDF();
    private List<db.ClientsDDF.ClientDDFs> _data = new List<db.ClientsDDF.ClientDDFs>();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDataSource();

            Session["DialogDataSource"] = order;
           
        }

        OrdersGrid.EditSettings.EditMode = Syncfusion.JavaScript.EditMode.DialogTemplate;
        OrdersGrid.EditSettings.DialogEditorTemplateID = "#Dialogtemplate";

    }

    private void BindDataSource()
    {
        _data = _clients.GetALLClinetListSource();

        foreach (db.ClientsDDF.ClientDDFs item in _data)
        {

            order.Add(new Orders(item.id, item.name, item.RegNo, item.VatNo, item.PhysicalAddress1, item.PhysicalAddress2, item.PhysicalAddress3, item.PhysicalAddress4, item.PhysicalAddressPostal, item.PhysicalAddressCity,
        item.PhysicalAddressCountry, item.ClientContact1, item.ClientContact1Tel, item.ClientContact1Email, item.ClientContact2, item.ClientContact2Tel, item.ClientContact2Email));

        }
        this.OrdersGrid.DataSource = order;
        this.OrdersGrid.DataBind();
    }

    protected void EditEvents_ServerEditRow(object sender, GridEventArgs e)
    {
        EditAction(e.EventType, e.Arguments["data"]);
    }

    protected void EditEvents_ServerAddRow(object sender, GridEventArgs e)
    {
        EditAction(e.EventType, e.Arguments["data"]);
    }

    protected void EditEvents_ServerDeleteRow(object sender, GridEventArgs e)
    {
        EditAction(e.EventType, e.Arguments["data"]);
    }

    protected void EditAction(string eventType, object record)
    {
        List<Orders> data = Session["DialogDataSource"] as List<Orders>;
        Dictionary<string, object> KeyVal = record as Dictionary<string, object>;
        db.ClientsDDF.ClientDDFs tempedit = new db.ClientsDDF.ClientDDFs();

        if (eventType == "endEdit")
        {
            Orders value = new Orders();

            foreach (KeyValuePair<string, object> keyval in KeyVal)
            {
                if (keyval.Key == "OrderID")
                {
                    // newRecord.OrderID = "-1";
                    value = data.Where(d => d.OrderID == (string)keyval.Value).FirstOrDefault();
                    tempedit.id = Convert.ToString(keyval.Value);
                }
                else if (keyval.Key == "CName")
                {
                    value.CName = Convert.ToString(keyval.Value);
                    tempedit.name = Convert.ToString(keyval.Value);
                }
                else if (keyval.Key == "RegNo")
                    tempedit.RegNo = Convert.ToString(keyval.Value);
                else if (keyval.Key == "VatNo")
                    tempedit.VatNo = Convert.ToString(keyval.Value);
                else if (keyval.Key == "PhysicalAddress1")
                    tempedit.PhysicalAddress1 = Convert.ToString(keyval.Value);
                else if (keyval.Key == "PhysicalAddress2")
                    tempedit.PhysicalAddress2 = Convert.ToString(keyval.Value);
                else if (keyval.Key == "PhysicalAddress3")
                    tempedit.PhysicalAddress3 = Convert.ToString(keyval.Value);
                else if (keyval.Key == "PhysicalAddress4")
                    tempedit.PhysicalAddress4 = Convert.ToString(keyval.Value);
                else if (keyval.Key == "PhysicalAddressPostal")
                    tempedit.PhysicalAddressPostal = Convert.ToString(keyval.Value);
                else if (keyval.Key == "PhysicalAddressCity")
                    tempedit.PhysicalAddressCity = Convert.ToString(keyval.Value);
                else if (keyval.Key == "PhysicalAddressCountry")
                    tempedit.PhysicalAddressCountry = Convert.ToString(keyval.Value);
                else if (keyval.Key == "ClientContact1")
                    tempedit.ClientContact1 = Convert.ToString(keyval.Value);
                else if (keyval.Key == "ClientContact1Tel")
                    tempedit.ClientContact1Tel = Convert.ToString(keyval.Value);
                else if (keyval.Key == "ClientContact1Email")
                    tempedit.ClientContact1Email = Convert.ToString(keyval.Value);
                else if (keyval.Key == "ClientContact2")
                    tempedit.ClientContact2 = Convert.ToString(keyval.Value);
                else if (keyval.Key == "ClientContact2Tel")
                    tempedit.ClientContact2Tel = Convert.ToString(keyval.Value);
                else if (keyval.Key == "ClientContact2Email")
                    tempedit.ClientContact2Email = Convert.ToString(keyval.Value);
            }
        }

        else if (eventType == "endAdd")
        {
            Orders newRecord = new Orders();

            tempedit.id = "-1";
            newRecord.OrderID = "-1";
            newRecord.CName = "-1";

            foreach (KeyValuePair<string, object> keyval in KeyVal)
            {

                //if (keyval.Value != string.Empty)
                //{
                if (keyval.Key == "OrderID")
                {
                    tempedit.id = "-1";
                }
                else if (keyval.Key == "CName")
                {
                    //  newRecord.CName = Convert.ToString(keyval.Value);
                    tempedit.name = Convert.ToString(keyval.Value);
                    newRecord.CName = Convert.ToString(keyval.Value);
                }
                else if (keyval.Key == "RegNo")
                    tempedit.RegNo = Convert.ToString(keyval.Value);
                else if (keyval.Key == "VatNo")
                    tempedit.VatNo = Convert.ToString(keyval.Value);
                else if (keyval.Key == "PhysicalAddress1")
                    tempedit.PhysicalAddress1 = Convert.ToString(keyval.Value);
                else if (keyval.Key == "PhysicalAddress2")
                    tempedit.PhysicalAddress2 = Convert.ToString(keyval.Value);
                else if (keyval.Key == "PhysicalAddress3")
                    tempedit.PhysicalAddress3 = Convert.ToString(keyval.Value);
                else if (keyval.Key == "PhysicalAddress4")
                    tempedit.PhysicalAddress4 = Convert.ToString(keyval.Value);
                else if (keyval.Key == "PhysicalAddressPostal")
                    tempedit.PhysicalAddressPostal = Convert.ToString(keyval.Value);
                else if (keyval.Key == "PhysicalAddressCity")
                    tempedit.PhysicalAddressCity = Convert.ToString(keyval.Value);
                else if (keyval.Key == "PhysicalAddressCountry")
                    tempedit.PhysicalAddressCountry = Convert.ToString(keyval.Value);
                else if (keyval.Key == "ClientContact1")
                    tempedit.ClientContact1 = Convert.ToString(keyval.Value);
                else if (keyval.Key == "ClientContact1Tel")
                    tempedit.ClientContact1Tel = Convert.ToString(keyval.Value);
                else if (keyval.Key == "ClientContact1Email")
                    tempedit.ClientContact1Email = Convert.ToString(keyval.Value);
                else if (keyval.Key == "ClientContact2")
                    tempedit.ClientContact2 = Convert.ToString(keyval.Value);
                else if (keyval.Key == "ClientContact2Tel")
                    tempedit.ClientContact2Tel = Convert.ToString(keyval.Value);
                else if (keyval.Key == "ClientContact2Email")
                    tempedit.ClientContact2Email = Convert.ToString(keyval.Value);


            }
            //}
            data.Insert(0, newRecord);
        }

        _clients.CreateAndUpdateClients(tempedit);

        Session["DialogDataSource"] = data;
        this.OrdersGrid.DataSource = data;
        this.OrdersGrid.DataBind();
    }

    [Serializable]
    public class Orders
    {
        public Orders()
        {

        }
        public Orders(string orderId, string name, string regNo, string vatNo, string physicalAddress1, string physicalAddress2, string physicalAddress3, string physicalAddress4, string physicalAddressPostal,
            string physicalAddressCity, string physicalAddressCountry, string clientContact1, string clientContact1Tel, string clientContact1Email, string clientContact2, string clientContact2Tel, string clientContact2Email)
        {
            this.OrderID = orderId;
            this.CName = name;
            this.RegNo = regNo;
            this.VatNo = vatNo;
            this.PhysicalAddress1 = physicalAddress1;
            this.PhysicalAddress2 = physicalAddress2;
            this.PhysicalAddress3 = physicalAddress3;
            this.PhysicalAddress4 = physicalAddress4;
            this.PhysicalAddressPostal = physicalAddressPostal;
            this.PhysicalAddressCity = physicalAddressCity;
            this.PhysicalAddressCountry = physicalAddressCountry;
            this.ClientContact1 = clientContact1;
            this.ClientContact1Tel = clientContact1Tel;
            this.ClientContact1Email = clientContact1Email;
            this.ClientContact2 = clientContact2;
            this.ClientContact2Tel = clientContact2Tel;
            this.ClientContact2Email = clientContact2Email;
        }

        public string OrderID { get; set; }
        public string CName { get; set; }
        public string RegNo { get; set; }
        public string VatNo { get; set; }
        public string PhysicalAddress1 { get; set; }
        public string PhysicalAddress2 { get; set; }
        public string PhysicalAddress3 { get; set; }
        public string PhysicalAddress4 { get; set; }
        public string PhysicalAddressPostal { get; set; }
        public string PhysicalAddressCity { get; set; }
        public string PhysicalAddressCountry { get; set; }
        public string ClientContact1 { get; set; }
        public string ClientContact1Tel { get; set; }
        public string ClientContact1Email { get; set; }
        public string ClientContact2 { get; set; }
        public string ClientContact2Tel { get; set; }
        public string ClientContact2Email { get; set; }

    }
}
