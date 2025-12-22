using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web.UI.HtmlControls;

namespace HtmlPivot
{
    /// <summary>
    /// Create simple and advanced pivot reports.
    /// </summary>
    public class Pivot
    {
        #region Variables

        private DataTable _DataTable;
        private string _CssTopHeading;
        private string _CssSubHeading;
        private string _CssLeftColumn;
        private string _CssItems;
        private string _CssTotals;
        private string _CssTable;

        #endregion Variables

        #region Constructors

        public Pivot(DataTable dataTable)
        {
            Init();
            _DataTable = dataTable;
        }

        #endregion Constructors

        #region Properties

        public DataTable ResultTable
        {
            get { return _DataTable; }
        }

        public string CssTopHeading
        {
            get { return _CssTopHeading; }
            set { _CssTopHeading = value; }
        }

        public string CssSubHeading
        {
            get { return _CssSubHeading; }
            set { _CssSubHeading = value; }
        }

        public string CssLeftColumn
        {
            get { return _CssLeftColumn; }
            set { _CssLeftColumn = value; }
        }

        public string CssItems
        {
            get { return _CssItems; }
            set { _CssItems = value; }
        }

        public string CssTotals
        {
            get { return _CssTotals; }
            set { _CssTotals = value; }
        }

        public string CssTable
        {
            get { return _CssTable; }
            set { _CssTable = value; }
        }

        #endregion Properties

        #region Private Methods

        private string[] FindValues(string xAxisField, string xAxisValue, string yAxisField, string yAxisValue, string[] zAxisFields)
        {
            int zAxis = zAxisFields.Length;
            if (zAxis < 1)
                zAxis++;
            string[] zAxisValues = new string[zAxis];
            //set default values
            for (int i = 0; i <= zAxisValues.GetUpperBound(0); i++)
            {
                zAxisValues[i] = "0";
            }

            try
            {
                foreach (DataRow row in _DataTable.Rows)
                {
                    if (Convert.ToString(row[xAxisField]) == xAxisValue && Convert.ToString(row[yAxisField]) == yAxisValue)
                    {
                        for (int z = 0; z < zAxis; z++)
                        {
                            zAxisValues[z] = Convert.ToString(row[zAxisFields[z]]);
                        }
                        break;
                    }
                }
            }
            catch
            {
                throw;
            }

            return zAxisValues;
        }

        private string FindValue(string xAxisField, string xAxisValue, string yAxisField, string yAxisValue, string zAxisField)
        {
            string zAxisValue = "";

            try
            {
                foreach (DataRow row in _DataTable.Rows)
                {
                    if (Convert.ToString(row[xAxisField]) == xAxisValue && Convert.ToString(row[yAxisField]) == yAxisValue)
                    {
                        zAxisValue = Convert.ToString(row[zAxisField]);
                        break;
                    }
                }
            }
            catch
            {
                throw;
            }

            return zAxisValue;
        }

        private void Init()
        {
            _CssTopHeading = "";
            _CssSubHeading = "";
            _CssLeftColumn = "";
            _CssItems = "";
            _CssTotals = "";
            _CssTable = "";
        }

        private void TableStyle(HtmlTable table)
        {
            if (_CssTable == "")
            {
                table.Style.Add("border-style", "solid");
                table.Style.Add("border-width", "1px");
                table.Style.Add("border-color", "black");
            }
            else
                table.Attributes.Add("Class", _CssTable);
        }

        private void MainHeaderTopCellStyle(HtmlTableCell cell)
        {
            if (_CssTopHeading == "")
            {
                cell.Style.Add("font-family", "tahoma");
                cell.Style.Add("font-size", "10pt");
                cell.Style.Add("font-weight", "normal");
                cell.Style.Add("background-color", "black");
                cell.Style.Add("color", "white");
                cell.Style.Add("text-align", "center");
            }
            else
                cell.Attributes.Add("Class", _CssTopHeading);
        }

        private void MainHeaderLeftCellStyle(HtmlTableCell cell)
        {
            if (_CssLeftColumn == "")
            {
                cell.Style.Add("font-family", "tahoma");
                cell.Style.Add("font-size", "10pt");
                cell.Style.Add("font-weight", "normal");
                cell.Style.Add("background-color", "black");
                cell.Style.Add("color", "white");
            }
            else
                cell.Attributes.Add("Class", _CssLeftColumn);
        }

        private void SubHeaderCellStyle(HtmlTableCell cell)
        {
            if (_CssSubHeading == "")
            {
                cell.Style.Add("font-family", "tahoma");
                cell.Style.Add("font-size", "10pt");
                cell.Style.Add("font-weight", "normal");
                cell.Style.Add("background-color", "#8ca6ce");
                cell.Style.Add("color", "black");
            }
            else
                cell.Attributes.Add("Class", _CssSubHeading);
        }

        private void ItemCellStyle(HtmlTableCell cell)
        {
            if (_CssItems == "")
            {
                cell.Style.Add("font-family", "tahoma");
                cell.Style.Add("font-size", "8pt");
                cell.Style.Add("font-weight", "normal");
                cell.Style.Add("background-color", "#dde4f0");
                cell.Style.Add("color", "black");
            }
            else
                cell.Attributes.Add("Class", _CssItems);
        }

        private void TotalCellStyle(HtmlTableCell cell)
        {
            if (_CssTotals == "")
            {
                cell.Style.Add("font-family", "tahoma");
                cell.Style.Add("font-size", "8pt");
                cell.Style.Add("font-weight", "bold");
                cell.Style.Add("background-color", "#dde4f0");
                cell.Style.Add("color", "black");
            }
            else
                cell.Attributes.Add("Class", _CssTotals);
        }

        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// Creates an advanced 3D Pivot table.
        /// </summary>
        /// <param name="xAxisField">The main heading at the top of the report.</param>
        /// <param name="yAxisField">The heading on the left of the report.</param>
        /// <param name="zAxisFields">The sub heading at the top of the report.</param>
        /// <returns>HtmlTable Control.</returns>
        public HtmlTable PivotTable(string xAxisField, string yAxisField, string[] zAxisFields)
        {
            HtmlTable table = new HtmlTable();
            //style table
            TableStyle(table);
            /*
             * The x-axis is the main horizontal row.
             * The z-axis is the sub horizontal row.
             * The y-axis is the left vertical column.
             */

            try
            {
                //get distinct xAxisFields
                ArrayList xAxis = new ArrayList();
                foreach (DataRow row in _DataTable.Rows)
                {
                    if (!xAxis.Contains(row[xAxisField]))
                        xAxis.Add(row[xAxisField]);
                }

                //get distinct yAxisFields
                ArrayList yAxis = new ArrayList();
                foreach (DataRow row in _DataTable.Rows)
                {
                    if (!yAxis.Contains(row[yAxisField]))
                        yAxis.Add(row[yAxisField]);
                }

                //create a 2D array for the y-axis/z-axis fields
                int zAxis = zAxisFields.Length;
                if (zAxis < 1)
                    zAxis = 1;
                string[,] matrix = new string[(xAxis.Count * zAxis), yAxis.Count];
                string[] zAxisValues = new string[zAxis];

                for (int y = 0; y < yAxis.Count; y++) //loop thru y-axis fields
                {
                    //rows
                    for (int x = 0; x < xAxis.Count; x++) //loop thru x-axis fields
                    {
                        //main columns
                        //get the z-axis values
                        zAxisValues = FindValues(xAxisField, Convert.ToString(xAxis[x])
                            , yAxisField, Convert.ToString(yAxis[y]), zAxisFields);
                        for (int z = 0; z < zAxis; z++) //loop thru z-axis fields
                        {
                            //sub columns
                            matrix[(((x + 1) * zAxis - zAxis) + z), y] = zAxisValues[z];
                        }
                    }
                }

                //calculate totals for the y-axis
                decimal[] yTotals = new decimal[(xAxis.Count * zAxis)];
                for (int col = 0; col < (xAxis.Count * zAxis); col++)
                {
                    yTotals[col] = 0;
                    for (int row = 0; row < yAxis.Count; row++)
                    {
                        yTotals[col] += Convert.ToDecimal(matrix[col, row]);
                    }
                }

                //calculate totals for the x-axis
                decimal[,] xTotals = new decimal[zAxis, (yAxis.Count + 1)];
                for (int y = 0; y < yAxis.Count; y++) //loop thru the y-axis
                {
                    int zCount = 0;
                    for (int z = 0; z < (zAxis * xAxis.Count); z++) //loop thru the z-axis
                    {
                        xTotals[zCount, y] += Convert.ToDecimal(matrix[z, y]);
                        if (zCount == (zAxis - 1))
                            zCount = 0;
                        else
                            zCount++;
                    }
                }
                for (int xx = 0; xx < zAxis; xx++) //Grand Total
                {
                    for (int xy = 0; xy < yAxis.Count; xy++)
                    {
                        xTotals[xx, yAxis.Count] += xTotals[xx, xy];
                    }
                }

                //Build HTML Table
                //Append main row (x-axis)
                HtmlTableRow mainRow = new HtmlTableRow();
                mainRow.Cells.Add(new HtmlTableCell());
                for (int x = 0; x <= xAxis.Count; x++) //loop thru x-axis + 1
                {
                    HtmlTableCell cell = new HtmlTableCell();
                    cell.ColSpan = zAxis;
                    if (x < xAxis.Count)
                        cell.InnerText = Convert.ToString(xAxis[x]);
                    else
                        cell.InnerText = "Grand Totals";
                    //style cell
                    MainHeaderTopCellStyle(cell);
                    mainRow.Cells.Add(cell);
                }
                table.Rows.Add(mainRow);
                //Append sub row (z-axis)
                HtmlTableRow subRow = new HtmlTableRow();
                subRow.Cells.Add(new HtmlTableCell());
                subRow.Cells[0].InnerText = yAxisField;
                //style cell
                SubHeaderCellStyle(subRow.Cells[0]);
                for (int x = 0; x <= xAxis.Count; x++) //loop thru x-axis + 1
                {
                    for (int z = 0; z < zAxis; z++)
                    {
                        HtmlTableCell cell = new HtmlTableCell();
                        cell.InnerText = zAxisFields[z];
                        //style cell
                        SubHeaderCellStyle(cell);
                        subRow.Cells.Add(cell);
                    }
                }
                table.Rows.Add(subRow);
                //Append table items from matrix
                for (int y = 0; y < yAxis.Count; y++) //loop thru y-axis
                {
                    HtmlTableRow itemRow = new HtmlTableRow();
                    for (int z = 0; z <= (zAxis * xAxis.Count); z++) //loop thru z-axis + 1
                    {
                        HtmlTableCell cell = new HtmlTableCell();
                        if (z == 0)
                        {
                            cell.InnerText = Convert.ToString(yAxis[y]);
                            //style cell
                            MainHeaderLeftCellStyle(cell);
                        }
                        else
                        {
                            cell.InnerText = Convert.ToString(matrix[(z - 1), y]);
                            //style cell
                            ItemCellStyle(cell);
                        }
                        itemRow.Cells.Add(cell);
                    }
                    //append x-axis grand totals
                    for (int z = 0; z < zAxis; z++)
                    {
                        HtmlTableCell cell = new HtmlTableCell();
                        cell.InnerText = Convert.ToString(xTotals[z, y]);
                        //style cell
                        TotalCellStyle(cell);
                        itemRow.Cells.Add(cell);
                    }
                    table.Rows.Add(itemRow);
                }
                //append y-axis totals
                HtmlTableRow totalRow = new HtmlTableRow();
                for (int x = 0; x <= (zAxis * xAxis.Count); x++)
                {
                    HtmlTableCell cell = new HtmlTableCell();
                    if (x == 0)
                        cell.InnerText = "Totals";
                    else
                        cell.InnerText = Convert.ToString(yTotals[x - 1]);
                    //style cell
                    TotalCellStyle(cell);
                    totalRow.Cells.Add(cell);
                }
                //append x-axis/y-axis totals
                for (int z = 0; z < zAxis; z++)
                {
                    HtmlTableCell cell = new HtmlTableCell();
                    cell.InnerText = Convert.ToString(xTotals[z, xTotals.GetUpperBound(1)]);
                    //style cell
                    TotalCellStyle(cell);
                    totalRow.Cells.Add(cell);
                }
                table.Rows.Add(totalRow);
            }
            catch
            {
                throw;
            }

            return table;
        }

        /// <summary>
        /// Creates a simple 3D Pivot Table.
        /// </summary>
        /// <param name="xAxisField">The heading at the top of the table.</param>
        /// <param name="yAxisField">The heading to the left of the table.</param>
        /// <param name="zAxisField">The item value field.</param>
        /// <returns></returns>
        public HtmlTable PivotTable(string xAxisField, string yAxisField, string zAxisField)
        {
            HtmlTable table = new HtmlTable();
            //style table
            TableStyle(table);
            /*
             * The x-axis is the main horizontal row.
             * The z-axis is the sub horizontal row.
             * The y-axis is the left vertical column.
             */

            try
            {
                //get distinct xAxisFields
                ArrayList xAxis = new ArrayList();
                foreach (DataRow row in _DataTable.Rows)
                {
                    if (!xAxis.Contains(row[xAxisField]))
                        xAxis.Add(row[xAxisField]);
                }

                //get distinct yAxisFields
                ArrayList yAxis = new ArrayList();
                foreach (DataRow row in _DataTable.Rows)
                {
                    if (!yAxis.Contains(row[yAxisField]))
                        yAxis.Add(row[yAxisField]);
                }

                //create a 2D array for the x-axis/y-axis fields
                string[,] matrix = new string[xAxis.Count, yAxis.Count];
                string zAxisValue = "";

                for (int y = 0; y < yAxis.Count; y++) //loop thru y-axis fields
                {
                    //rows
                    for (int x = 0; x < xAxis.Count; x++) //loop thru x-axis fields
                    {
                        //main columns
                        //get the z-axis values
                        zAxisValue = FindValue(xAxisField, Convert.ToString(xAxis[x])
                            , yAxisField, Convert.ToString(yAxis[y]), zAxisField);
                        matrix[x, y] = zAxisValue;
                    }
                }

                //calculate totals for the y-axis
                decimal[] yTotals = new decimal[xAxis.Count];
                for (int col = 0; col < xAxis.Count; col++)
                {
                    yTotals[col] = 0;
                    for (int row = 0; row < yAxis.Count; row++)
                    {
                        yTotals[col] += Convert.ToDecimal(matrix[col, row]);
                    }
                }

                //calculate totals for the x-axis
                decimal[] xTotals = new decimal[(yAxis.Count + 1)];
                for (int row = 0; row < yAxis.Count; row++)
                {
                    xTotals[row] = 0;
                    for (int col = 0; col < xAxis.Count; col++)
                    {
                        xTotals[row] += Convert.ToDecimal(matrix[col, row]);
                    }
                }
                xTotals[xTotals.GetUpperBound(0)] = 0; //Grand Total
                for (int i = 0; i < xTotals.GetUpperBound(0); i++)
                {
                    xTotals[xTotals.GetUpperBound(0)] += xTotals[i];
                }

                //Build HTML Table
                HtmlTableRow heading = new HtmlTableRow();
                for (int x = 0; x < (xAxis.Count + 1); x++)
                {
                    HtmlTableCell cell = new HtmlTableCell();
                    if (x == 0)
                        cell.InnerText = yAxisField;
                    else
                        cell.InnerText = Convert.ToString(xAxis[(x - 1)]);
                    //style cell
                    MainHeaderTopCellStyle(cell);
                    heading.Cells.Add(cell);
                }
                //append grand totals heading
                HtmlTableCell grandTotal = new HtmlTableCell();
                grandTotal.InnerText = "Grand Totals";
                //style cell
                MainHeaderTopCellStyle(grandTotal);
                heading.Cells.Add(grandTotal);

                table.Rows.Add(heading);

                for (int y = 0; y < yAxis.Count; y++)
                {
                    HtmlTableRow row = new HtmlTableRow();
                    for (int x = 0; x < (xAxis.Count + 1); x++)
                    {
                        HtmlTableCell cell = new HtmlTableCell();
                        if (x == 0)
                        {
                            cell.InnerText = Convert.ToString(yAxis[y]);
                            //style cell
                            MainHeaderLeftCellStyle(cell);
                        }
                        else
                        {
                            cell.InnerText = Convert.ToString(matrix[(x - 1), y]);
                            //style cell
                            ItemCellStyle(cell);
                        }
                        row.Cells.Add(cell);
                    }
                    //append x-axis totals
                    HtmlTableCell totalCell = new HtmlTableCell();
                    totalCell.InnerText = Convert.ToString(xTotals[y]);
                    //style cell
                    TotalCellStyle(totalCell);
                    row.Cells.Add(totalCell);
                    table.Rows.Add(row);
                }
                //append y-axis totals
                HtmlTableRow totalRow = new HtmlTableRow();
                for (int x = 0; x <= (xAxis.Count + 1); x++)
                {
                    HtmlTableCell cell = new HtmlTableCell();
                    if (x == 0)
                        cell.InnerText = "Totals";
                    else
                        if (x <= xAxis.Count)
                            cell.InnerText = Convert.ToString(yTotals[(x - 1)]);
                        else
                            cell.InnerText = Convert.ToString(xTotals[xTotals.GetUpperBound(0)]);
                    //style cell
                    TotalCellStyle(cell);
                    totalRow.Cells.Add(cell);
                }
                table.Rows.Add(totalRow);
            }
            catch
            {
                throw;
            }

            return table;
        }

        #endregion Public Methods
    }
}