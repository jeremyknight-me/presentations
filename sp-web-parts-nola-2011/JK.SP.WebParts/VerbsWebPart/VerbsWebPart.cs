using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace JK.SP.WebParts.VerbsWebPart
{
    [ToolboxItemAttribute(false)]
    public class VerbsWebPart : WebPart
    {
        public override WebPartVerbCollection Verbs
        {
            get
            {
                List<WebPartVerb> customVerbs = new List<WebPartVerb>();

                WebPartVerb serverSideVerb
                    = new WebPartVerb(this.ID, new WebPartEventHandler(this.ServerSideHandler));
                serverSideVerb.Text = "Server Side Verb";
                serverSideVerb.Visible = true;
                serverSideVerb.Description = "This click will execute server side code";
                customVerbs.Add(serverSideVerb);

                WebPartVerb clientSideVerb
                    = new WebPartVerb(this.ID + "newone", "alert('You clicked a client side verb!');");
                clientSideVerb.Text = "Client Side Verb";
                clientSideVerb.Visible = true;
                clientSideVerb.Description = "This click will execute client side code";
                customVerbs.Add(clientSideVerb);

                WebPartVerbCollection allVerbs
                    = new WebPartVerbCollection(base.Verbs, customVerbs);

                return allVerbs;
            }
        }

        protected void ServerSideHandler(object sender, WebPartEventArgs e)
        {
            try
            {
                TextBox textBox = new TextBox();
                textBox.TextMode = TextBoxMode.MultiLine;
                textBox.ID = "txtName";
                textBox.Text = "You clicked a server side verb!";
                Panel panel = new Panel();
                panel.Controls.Add(textBox);
                Controls.Add(panel);
            }
            catch (Exception ex)
            {
                WebPartUtil.HandleException(this, ex);
            }
        }
    }
}
