using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class BookStore : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) //on 1st Page Load
        {
            //get all books in the catalog.
            List<Book> books = BookCatalogDataAccess.GetAllBooks();

            foreach (Book book in books)
            {
                //todo: Populate dropdown list selections
                ListItem item = new ListItem(book.Title, book.Id);
                drpBookSelection.Items.Add(item);
            }
        }
        ShoppingCart shoppingcart = null;

        if (Session["shoppingcart"] == null)
        {
            shoppingcart = new ShoppingCart();
            //todo: add cart to the session
            Session["shoppingcart"] = shoppingcart;
        }
        else
        {
            //todo: retrieve cart from the session
            shoppingcart = Session["shoppingcart"] as ShoppingCart;

            foreach(BookOrder order in shoppingcart.BookOrders)
            {
                string bookId = order.Book.Id;

                //todo: Remove the book in the order from the dropdown list
                ListItem itemToRemove = drpBookSelection.Items.FindByValue(bookId);
                drpBookSelection.Items.Remove(itemToRemove);
            }
        }

        if (shoppingcart.NumOfItems == 0)
            lblNumItems.Text = "empty";
        else if (shoppingcart.NumOfItems == 1)
            lblNumItems.Text = "1 item";
        else
            lblNumItems.Text = shoppingcart.NumOfItems.ToString() + " items";
    }
    protected void drpBookSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpBookSelection.SelectedValue != "-1")
        {
            string bookId = drpBookSelection.SelectedItem.Value;
            Book selectedBook = BookCatalogDataAccess.GetBookById(bookId);

            //todo: Add selected book to the session
            Session["selectedbook"] = selectedBook;

            //todo: Display the selected book's description and price
            lblDescription.Text = selectedBook.Description;
            lblPrice.Text = "$"+selectedBook.Price.ToString();

        }
        else
        {
            //todo: Set description and price to blank
            lblDescription.Text = "";
            lblPrice.Text = "";
        }
    }
    protected void btnAddToCart_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (drpBookSelection.SelectedValue != "-1" && Session["selectedbook"] != null)
            {
                //todo: Retrieve selected book from the session
                Book selectedBook = Session["selectedbook"] as Book;

                //todo: get user entered quqntity
                int quantity = int.Parse(txtQuantity.Text);

                //todo: Create a book order with selected book and quantity
                BookOrder order = new BookOrder(selectedBook, quantity);

                //todo: Retrieve to cart from the session
                ShoppingCart shoppingcart = Session["shoppingcart"] as ShoppingCart;

                //todo: Add book order to the shopping cart
                shoppingcart.AddBookOrder(order);

                //todo: Remove the selected item from the dropdown list
                drpBookSelection.Items.Remove(drpBookSelection.SelectedItem);

                //todo: Set the dropdown list's selected value as "-1"
                drpBookSelection.SelectedIndex = 0;

                //todo: Set the description to show title and quantity of the book user added to the shopping cart
                lblDescription.Text = quantity.ToString() + (quantity == 1 ? " copy " : " copies ") + " of " + selectedBook.Title + " is ordered";

                //todo: Update the number of items in shopping cart displayed next to the link to ShoppingCartView.aspx
                if (shoppingcart.NumOfItems == 0)
                    lblNumItems.Text = "empty";
                else if (shoppingcart.NumOfItems == 1)
                    lblNumItems.Text = "1 item";
                else
                    lblNumItems.Text = shoppingcart.NumOfItems.ToString() + " items";
            }
        }
    }

    protected void btnViewCart_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShoppingCartView.aspx");
    }
}