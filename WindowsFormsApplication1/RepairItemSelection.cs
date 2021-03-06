﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GuitarShop
{
    /// <summary>
    /// Form for creating and modifying Orders.
    /// </summary>
    public partial class RepairItemSelection : Form
    {
        RepairForm parent;

        RepairItem repairItem;

        static SqlConnection cnn;

        public RepairItemSelection(RepairForm parent)
        {
            InitializeComponent();
            this.parent = parent;

            repairItem = new RepairItem();

            cnn = new SqlConnection(Constants.ConnectionString);

            try
            {
                cnn.Open();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: the connection could not be opened.");
                Close();
            }

            loadItems();
        }

        /// <summary>
        /// Ensure that form is ready to submit before allowing the user to do so,
        /// </summary>
        private void ValidateForm()
        {
            if (cmb_Item.SelectedIndex > -1
                && txt_repairType.Text != "")
            {
                btn_add.Enabled = true;
            }
            else
            {
                btn_add.Enabled = false;
            }
        }

        private void loadItems()
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = cnn;
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT ProductID, ProductName FROM Products WHERE ProductType = 'part'";

                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ComboBoxItem cbi = new ComboBoxItem(reader[1].ToString(), Convert.ToInt32(reader[0]));
                            cmb_Item.Items.Add(cbi);
                            
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not open Products.");
                }
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            repairItem.LaborPrice = updn_labor.Value;

            parent.AddRepairItem(repairItem);

            cnn.Close();
            Close();
        }

        private void cmb_Item_SelectedIndexChanged(object sender, EventArgs e)
        {
            repairItem.ProductID = (cmb_Item.SelectedItem as ComboBoxItem).IdentifyingValue;
            ValidateForm();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            cnn.Close();
            Close();
        }

        private void txt_repairType_TextChanged(object sender, EventArgs e)
        {
            repairItem.RepairType = txt_repairType.Text;
            ValidateForm();
        }
    }
}
