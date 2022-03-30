using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SACtask3
{
    public partial class Form1 : Form
    {

        string filter;
        List<Sales> sales = new List<Sales>();
        BindingSource bs = new BindingSource();

        public Form1()
        {
            InitializeComponent();
            LoadCSV();
            bs.DataSource = sales;
            dataGridView1.DataSource = bs;
        }

        private void LoadCSV()
        {
            string filePath = @"C:\Users\tomty\OneDrive\Documents\school\compu\Task3_Shop_Data.csv";
            List<string> lines = new List<string>();
            lines = File.ReadAllLines(filePath).ToList();
            foreach (string line in lines)
            {
                List<string> fields = line.Split(',').ToList();
                Sales s = new Sales();
                s.ItemName = fields[0];
                s.Subject = fields[1];
                s.Seller = fields[2];
                s.Purchaser = fields[3];
                s.Purchase = float.Parse(fields[4]);
                s.Sale = fields[5];
                s.Rating = fields[6];
                sales.Add(s);
            }


        }


        private void label1_Click(object sender, EventArgs e)

        {

        }

        private void cbo_filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            filter = cbo_filter.Text;
            if (filter == "Rating") SortByRating(sales);
            dataGridView1.DataSource = bs;
            bs.ResetBindings(false);
        }

        private void SortByRating(List<Sales> _sales)
        {
            int min;
            string temp;
            for (int i = 0; i < _sales.Count - 1; i++)
            {
                min = i;
                for (int j = i + 1; j < _sales.Count; j++)
                {

                    if (int.TryParse(_sales[j].Rating, out int ratingJ))
                    {

                        if (int.TryParse(_sales[min].Rating, out int ratingMin))
                        { if (ratingJ < ratingMin) min = j; }
                        else
                        {
                            min = j;
                        }
                    }
                }
                temp = _sales[min].Rating;
                _sales[min].Rating = _sales[i].Rating;
                _sales[i].Rating = temp;


            }
        }

        private List<Sales> Search(string target, string filter)
        {
            List<Sales> results = new List<Sales>();
            foreach (Sales s in sales)
            {
                if (filter == "Rating")
                {
                    if (s.Rating.ToLower() == target.ToLower()) results.Add(s);
                }
                if (filter == "Subject")
                {
                    if (s.Subject.ToLower().Contains(target.ToLower())) { results.Add(s); Console.WriteLine(target); }
                }
                if (filter == "Textbook")
                {
                    if (s.ItemName.ToLower().Contains(target.ToLower())) { results.Add(s); Console.WriteLine(target); }
                }

            }
            return results;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            List<Sales> r = Search(txt_search.Text, filter);
            bs.DataSource = r;
            dataGridView1.DataSource = r;
            bs.ResetBindings(false);
        }
    }
}
