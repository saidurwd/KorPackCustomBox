using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class captcha_captcha : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Bitmap bmap = new Bitmap(118, 50);
        Graphics grfs = Graphics.FromImage(bmap);
        grfs.Clear(Color.White);
        Random rnd = new Random();
        grfs.DrawLine(Pens.Black, rnd.Next(0, 50), rnd.Next(10, 30), rnd.Next(0, 200), rnd.Next(0, 50));
        grfs.DrawRectangle(Pens.White, rnd.Next(0, 20), rnd.Next(0, 20), rnd.Next(50, 80), rnd.Next(0, 20));
        grfs.DrawLine(Pens.Red, rnd.Next(0, 50), rnd.Next(10, 30), rnd.Next(0, 200), rnd.Next(0, 20));
        string str = string.Format("{0:X}", rnd.Next(100000, 999999));
        Session["verify"] = str.ToLower();
        Font fnt = new Font("Arial", 20, FontStyle.Bold);
        grfs.DrawString(str, fnt, Brushes.Red, 30, 20);
        // grfs.DrawString(text.Substring(0,3),fnt, Brushes.Navy, 20, 20);
        //grfs.DrawString(text.Substring(3),fnt, Brushes.Navy, 50, 20);
        bmap.Save(Response.OutputStream, ImageFormat.Gif);
    }
}