using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShoppingCartView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["shoppingcart"] == null)
        {
            //todo: Redirect the user to Bookstore.aspx page
            Response.Redirect("Bookstore.aspx");
        }
        //else
        //{
            //todo: Retrieve shopping cart from the session
            //ShoppingCart shoppingcart = Session["shoppingcart"] as ShoppingCart;

            //todo: Call DisplayShoppingCart method to display shopping cart 
            //DisplayShoppingCart(shoppingcart);
        //}

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        //todo: Redirect the user to Bookstore.aspx page
        Response.Redirect("Bookstore.aspx");
    }

    protected void btnEmptyShoppingCart_Click(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            //todo: Retrieve shopping cart from session.
            ShoppingCart shoppingcart = Session["shoppingcart"] as ShoppingCart;

            //todo: Clear shopping cart
            shoppingcart.BookOrders.Clear();
        }
    }

    private void Page_PreRender(object sender, EventArgs e)
    {
        ShoppingCart shoppingcart = Session["shoppingcart"] as ShoppingCart;
        if (shoppingcart.IsEmpty)
        {
            TableRow lastRow = new TableRow();
            TableCell lastRowCell = new TableCell();
            lastRowCell.Text = "Your Shopping Cart is Empty";
            lastRowCell.ForeColor = System.Drawing.Color.Red;
            lastRowCell.ColumnSpan = 3;
            lastRowCell.HorizontalAlign = HorizontalAlign.Center;
            lastRow.Cells.Add(lastRowCell);
            tblShoppingCart.Rows.Add(lastRow);
        }
        else
        {
            foreach (BookOrder order in shoppingcart.BookOrders)
            {
                TableRow row = new TableRow();

                TableCell cell = new TableCell();
                cell.Text = order.Book.Title;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = order.NumOfCopies.ToString();
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = "$" + order.NumOfCopies * order.Book.Price;
                row.Cells.Add(cell);

                tblShoppingCart.Rows.Add(row);
            }
            TableRow lastRow = new TableRow();
            TableCell lastRowCell = new TableCell();
            lastRowCell.Text = "Total";
            lastRowCell.ColumnSpan = 2;
            lastRowCell.HorizontalAlign = HorizontalAlign.Right;
            lastRow.Cells.Add(lastRowCell);

            lastRowCell = new TableCell();
            lastRowCell.Text = "$" + shoppingcart.TotalAmountPayable.ToString();
            lastRow.Cells.Add(lastRowCell);

            tblShoppingCart.Rows.Add(lastRow);
        }
    }
}