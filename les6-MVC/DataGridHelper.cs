using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI;

namespace UCLL.Helpers {
    public static class DataGridHelper {
        public static HtmlString DataGrid<T>(this HtmlHelper helper) {
            return DataGrid<T>(helper, null, (IEnumerable<MvcDataGridColumn>)null);
        }

        public static HtmlString DataGrid<T>(this HtmlHelper helper, object data) {
            return DataGrid<T>(helper, data, (IEnumerable<MvcDataGridColumn>)null);
        }

        public static HtmlString DataGrid<T>(this HtmlHelper helper, object data, IEnumerable<string> columns) {
            IEnumerable<MvcDataGridColumn> cols = null;
            if (columns != null) {
                cols = Convert<T>(columns);
            }
            return DataGrid<T>(helper, data, cols);
        }

        private static IEnumerable<MvcDataGridColumn> Convert<T>(IEnumerable<string> columns) {
            List<MvcDataGridColumn> ret = new List<MvcDataGridColumn>();
            foreach (string c in columns) {
                ret.Add(new MvcDataGridColumn(c));
            }
            return ret;
        }

        public static HtmlString DataGrid<T>(this HtmlHelper helper, object data, IEnumerable<MvcDataGridColumn> columns) {
            // Get items
            var items = data as IEnumerable<T>;
            if (items == null)
                items = helper.ViewData.Model as IEnumerable<T>;

            // Get column names
            if (columns == null) {
                columns = Convert<T>(typeof(T).GetProperties().Select(p => p.Name));
            }

            // Create HtmlTextWriter
            var writer = new HtmlTextWriter(new StringWriter());

            // Open table tag
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "table table-striped");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);

            // Render table header
            writer.RenderBeginTag(HtmlTextWriterTag.Thead);
            RenderHeader(helper, writer, columns);
            writer.RenderEndTag();

            // Render table body
            writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
            foreach (var item in items)
                RenderRow(helper, writer, columns, item);
            writer.RenderEndTag();

            // Close table tag
            writer.RenderEndTag();

            //Return the string
            return new HtmlString(writer.InnerWriter.ToString());
        }

        private static void RenderHeader(HtmlHelper helper, HtmlTextWriter writer, IEnumerable<MvcDataGridColumn> columns) {
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            foreach (var column in columns) {
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(helper.Encode(column.ColumnTitle));
                writer.RenderEndTag();
            }
            writer.RenderEndTag();
        }

        private static void RenderRow<T>(HtmlHelper helper, HtmlTextWriter writer, IEnumerable<MvcDataGridColumn> columns, T item) {
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            foreach (var column in columns) {
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                column.RenderCell(helper, writer, item);
                writer.RenderEndTag();
            }
            writer.RenderEndTag();
        }
    }

    public class MvcDataGridColumn {
        public MvcDataGridColumn(string columnName, string columnTitle = null, string format = null, IFormatProvider formatProvider = null) {
            if (columnName == null)
                throw new ArgumentNullException("columnName");
            _columnName = columnName;
            _columnTitle = columnTitle;
            _format = format;
            _formatProvider = formatProvider;
        }

        public virtual void RenderCell(HtmlHelper helper, HtmlTextWriter writer, object item) {
            var value = item.GetType().GetProperty(_columnName).GetValue(item, null) ?? string.Empty;
            string v = null;
            if (_format != null) {
                IFormattable valueForm = value as IFormattable;
                if (valueForm != null)
                    v = valueForm.ToString(Format, FormatProvider);
            }
            writer.Write(helper.Encode(v ?? value.ToString()));
        }

        public string ColumnName {
            get {
                return _columnName;
            }
        }
        private string _columnName;

        public string ColumnTitle {
            get {
                return _columnTitle ?? _columnName;
            }
        }
        private string _columnTitle;

        public string Format {
            get {
                return _format;
            }
        }
        private string _format;

        public IFormatProvider FormatProvider {
            get {
                return _formatProvider;
            }
        }
        private IFormatProvider _formatProvider;
    }

    public class MvcDataGridActionColumn : MvcDataGridColumn {
        public MvcDataGridActionColumn(string actionText, string action, string controller, string idProperty)
            : base(string.Empty) {
            _actionText = actionText;
            _action = action;
            _controller = controller;
            _idProperty = idProperty;
        }

        public override void RenderCell(HtmlHelper helper, HtmlTextWriter writer, object item) {
            var key = item.GetType().GetProperty(_idProperty).GetValue(item, null) ?? string.Empty;
            writer.Write(helper.ActionLink(ActionText, Action, Controller, new { id = key }, null));
        }

        public string ActionText {
            get {
                return _actionText;
            }
        }
        private string _actionText;

        public string Action {
            get {
                return _action;
            }
        }
        private string _action;

        public string Controller {
            get {
                return _controller;
            }
        }
        private string _controller;

        public string IdProperty {
            get {
                return _idProperty;
            }
        }
        private string _idProperty;
    }
}