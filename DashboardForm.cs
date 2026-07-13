using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace SonicWallManager
{
    public partial class DashboardForm : Form
    {
        private readonly HttpClient client;
        private readonly string firewallIp;
        public DashboardForm(HttpClient httpClient, string ip)
        {
            InitializeComponent();
            client = httpClient;
            firewallIp = ip;
            lblIp.Text = "Connected to: " + firewallIp;
        }

        private async Task CommitChanges()
        {
            var content = new StringContent("{}", Encoding.UTF8, "application/json");
            await client.PostAsync("/api/sonicos/config/pending", content);
        }

        private List<MacEntryModel> pendingMacs = new List<MacEntryModel>();
        //Create address object and Group EDUEXAMMAC

        // Simple Model Class
        public class MacEntryModel
        {
            public bool IsSelected { get; set; } // This checkbox column
            public string Name { get; set; }
            public string Mac { get; set; }
        }

        //Network Interface Load Start
        public class InterfaceModel // Model for the Grid
        {
            public string Name { get; set; }
            public string Zone { get; set; }
            public string IpAddress { get; set; }
            public string Gateway { get; set; }
            public string SubnetMask { get; set; }
            public string Assignment { get; set; }
        }
        private void SetupInterfaceGrid()
        {
            gridInterfaces.Columns.Clear();
            gridInterfaces.AutoGenerateColumns = false;

            gridInterfaces.Columns.Add(new DataGridViewTextBoxColumn { Name = "colIfName", HeaderText = "Name", DataPropertyName = "Name" });
            gridInterfaces.Columns.Add(new DataGridViewTextBoxColumn { Name = "colIfZone", HeaderText = "Zone", DataPropertyName = "Zone" });
            gridInterfaces.Columns.Add(new DataGridViewTextBoxColumn { Name = "colIfIp", HeaderText = "IP Address", DataPropertyName = "IpAddress" });
            gridInterfaces.Columns.Add(new DataGridViewTextBoxColumn { Name = "colIfSubnet", HeaderText = "Subnet Mask", DataPropertyName = "SubnetMask" });
            gridInterfaces.Columns.Add(new DataGridViewTextBoxColumn { Name = "colIfGw", HeaderText = "Gateway", DataPropertyName = "Gateway" });
            gridInterfaces.Columns.Add(new DataGridViewTextBoxColumn { Name = "colIfAssign", HeaderText = "IP Assignment", DataPropertyName = "Assignment" });

            gridInterfaces.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "colIfEdit",
                HeaderText = "Configure",
                Text = "Edit",
                UseColumnTextForButtonValue = true,
                Width = 80
            });
        }

        private async Task LoadInterfacesFromFirewall()
        {
            try
            {
                lblStatus.Text = "Loading interfaces...";

                // URL from your Postman test
                var response = await client.GetAsync("/api/sonicos/interfaces/ipv4");

                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Failed to fetch interfaces: {error}");
                    return;
                }

                string json = await response.Content.ReadAsStringAsync();
                List<InterfaceModel> interfacesList = new List<InterfaceModel>();

                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    // FIX: In your JSON, "interfaces" is the Array.
                    // We do NOT call .GetProperty("ipv4") here.
                    var interfacesArray = doc.RootElement.GetProperty("interfaces");

                    foreach (var item in interfacesArray.EnumerateArray())
                    {
                        // Each item in the array has one property: "ipv4"
                        if (item.TryGetProperty("ipv4", out JsonElement ipv4Details))
                        {
                            string name = ipv4Details.GetProperty("name").GetString();

                            // Filter for X0 through X4
                            if (!(name == "X0" || name == "X1" || name == "X2" || name == "X3" || name == "X4"))
                                continue;

                            string ip = "N/A";
                            string mask = "N/A";
                            string zone = "N/A";
                            string gw = "N/A";
                            string modeText = "N/A";

                            if (ipv4Details.TryGetProperty("ip_assignment", out JsonElement assignment))
                            {
                                zone = assignment.TryGetProperty("zone", out var z) ? z.GetString() : "N/A";

                                if (assignment.TryGetProperty("mode", out JsonElement modeObj))
                                {
                                    // Check for Static
                                    if (modeObj.TryGetProperty("static", out JsonElement staticData))
                                    {
                                        ip = staticData.TryGetProperty("ip", out var ipVal) ? ipVal.GetString() : "N/A";
                                        mask = staticData.TryGetProperty("netmask", out var nmVal) ? nmVal.GetString() : "N/A";
                                        // PULL GATEWAY HERE
                                        gw = staticData.TryGetProperty("gateway", out var gwVal) ? gwVal.GetString() : "0.0.0.0";
                                        modeText = "Static";
                                    }
                                    // Check for DHCP (like your X1 interface)
                                    else if (modeObj.TryGetProperty("dhcp", out _))
                                    {
                                        ip = "DHCP";
                                        mask = "DHCP";
                                        gw = "DHCP";
                                        modeText = "DHCP";
                                    }
                                }
                            }

                            interfacesList.Add(new InterfaceModel
                            {
                                Name = name,
                                Zone = zone,
                                IpAddress = ip,
                                SubnetMask = mask,
                                Gateway = gw,
                                Assignment = modeText
                            });
                        }
                    }
                }

                gridInterfaces.DataSource = null;
                gridInterfaces.DataSource = interfacesList;
                lblStatus.Text = $"Loaded {interfacesList.Count} interfaces.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Interface Load Error: " + ex.Message);
            }
        }

        private async void btnLoadInterfaces_Click(object sender, EventArgs e)
        {
            SetupInterfaceGrid();
            await LoadInterfacesFromFirewall();
            
        }

        private async void gridInterfaces_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (gridInterfaces.Columns[e.ColumnIndex].Name == "colIfEdit")
            {
                var item = (InterfaceModel)gridInterfaces.Rows[e.RowIndex].DataBoundItem;

                using (Form f = new Form())
                {
                    f.Text = $"Configure {item.Name}";
                    f.Size = new Size(350, (item.Name == "X1") ? 480 : 300);
                    f.StartPosition = FormStartPosition.CenterParent;
                    f.FormBorderStyle = FormBorderStyle.FixedDialog;
                    f.MaximizeBox = false;

                    int top = 20;

                    // X1 Assignment Mode (Static vs DHCP)
                    ComboBox cmbMode = new ComboBox() { Left = 20, Top = top + 20, Width = 280, DropDownStyle = ComboBoxStyle.DropDownList };
                    cmbMode.Items.AddRange(new string[] { "static", "dhcp" });
                    cmbMode.SelectedItem = item.Assignment?.ToLower() == "dhcp" ? "dhcp" : "static";

                    if (item.Name == "X1")
                    {
                        f.Controls.Add(new Label() { Left = 20, Top = top, Text = "Mode (Static/DHCP):" });
                        f.Controls.Add(cmbMode);
                        top += 60;
                    }

                    // IP Address (Always shown)
                    f.Controls.Add(new Label() { Left = 20, Top = top, Text = "IP Address:" });
                    TextBox txtIp = new TextBox() { Left = 20, Top = top + 20, Text = item.IpAddress, Width = 280 };
                    f.Controls.Add(txtIp);
                    top += 50;

                    // Subnet Mask (Only for X0 and X1)
                    TextBox txtMask = new TextBox() { Left = 20, Top = top + 20, Text = item.SubnetMask, Width = 280 };
                    if (item.Name == "X0" || item.Name == "X1")
                    {
                        f.Controls.Add(new Label() { Left = 20, Top = top, Text = "Subnet Mask:" });
                        f.Controls.Add(txtMask);
                        top += 50;
                    }

                    // X1 Gateway
                    // Inside gridInterfaces_CellContentClick
                    TextBox txtGw = new TextBox() { Left = 20, Top = top + 20, Text = item.Gateway, Width = 280 }; // Use item.Gateway
                    if (item.Name == "X1")
                    {
                        f.Controls.Add(new Label() { Left = 20, Top = top, Text = "Default Gateway:" });
                        f.Controls.Add(txtGw);
                        top += 60;
                    }

                    Button btnOk = new Button() { Text = "Submit & Auto-Commit", Left = 20, Top = top, Width = 280, Height = 40, DialogResult = DialogResult.OK };
                    f.Controls.Add(btnOk);
                    f.AcceptButton = btnOk;

                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        if (cmbMode.SelectedItem?.ToString() == "static")
                        {
                            if (!IsValidIp(txtIp.Text))
                            {
                                MessageBox.Show("Invalid IP Address format. Please use a.b.c.d");
                                return; // Stops the update if IP is bad
                            }

                            if (!txtMask.Text.StartsWith("/") && !IsValidIp(txtMask.Text))
                            {
                                MessageBox.Show("Invalid Netmask format. Use 255.255.255.0 or /24");
                                return;
                            }

                            if (item.Name == "X1" && !IsValidIp(txtGw.Text))
                            {
                                MessageBox.Show("Invalid Gateway format.");
                                return;
                            }
                        }
                        try
                        {
                            this.Cursor = Cursors.WaitCursor;
                            bool isDhcp = (item.Name == "X1" && cmbMode.SelectedItem.ToString() == "dhcp");

                            // 1. Export Update to Firewall
                            await ExportComplexUpdate(item.Name, item.Zone, txtIp.Text, txtMask.Text, txtGw.Text, isDhcp);

                            // 2. Auto-Commit Changes
                            await CommitChanges();

                            // 3. Refresh Grid UI
                            await LoadInterfacesFromFirewall();

                            MessageBox.Show($"{item.Name} updated and committed successfully!");
                        }
                        catch (Exception ex) { MessageBox.Show("Update Failed: " + ex.Message); }
                        finally { this.Cursor = Cursors.Default; }
                    }
                }
            }
        }
        private async Task ExportComplexUpdate(string name, string zone, string ip, string mask, string gw, bool isDhcp)
        {
            // Build the mode payload based on selection
            object modePayload;
            if (isDhcp)
            {
                modePayload = new { dhcp = new { renew_on_startup = true } };
            }
            else
            {
                modePayload = new
                {
                    @static = new
                    {
                        ip = ip,
                        netmask = mask,
                        gateway = gw
                    }
                };
            }

            // Match your JSON structure exactly
            var payload = new
            {
                @interface = new
                {
                    ipv4 = new
                    {
                        name = name,
                        ip_assignment = new
                        {
                            zone = zone,
                            mode = modePayload
                        }
                    }
                }
            };

            string json = System.Text.Json.JsonSerializer.Serialize(payload);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            // FIXED URL: Added "/name/" to match your Postman test
            var response = await client.PutAsync($"/api/sonicos/interfaces/ipv4/name/{name}", content);

            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Firewall rejected update: {error}");
            }

        }
        private bool IsValidIp(string ip)
        {
            // SonicWall requires dotted decimal format a.b.c.d
            if (System.Net.IPAddress.TryParse(ip, out System.Net.IPAddress address))
            {
                return address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork;
            }
            return false;
        }
       
        private async void btnLogout_Click(object sender, EventArgs e)
        {
            await LogoutFirewall();

            client.Dispose();

            this.Close(); // close dashboard
        }

        // logout method 
        private async Task LogoutFirewall()
        {
            try
            {
                var body = new StringContent(
                    "{\"logout\": true}",
                    Encoding.UTF8,
                    "application/json");

                var response = await client.PostAsync("/api/sonicos/auth", body);

                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Firewall logout failed:\n{response.StatusCode}\n{error}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Logout exception: " + ex.Message);
            }
        }
        //end of network 

        public class AddressObjectModel
        {
            public string Name { get; set; }
            public string Zone { get; set; }
            public string Ip { get; set; }
            public string Uuid { get; set; } // Important for the PUT request
        }
        private void SetupSvrGrid()
        {
            gridSvrIps.Columns.Clear();
            gridSvrIps.AutoGenerateColumns = false;

            gridSvrIps.Columns.Add(new DataGridViewTextBoxColumn { Name = "colName", HeaderText = "Name", DataPropertyName = "Name" });
            gridSvrIps.Columns.Add(new DataGridViewTextBoxColumn { Name = "colZone", HeaderText = "Zone", DataPropertyName = "Zone" });
            gridSvrIps.Columns.Add(new DataGridViewTextBoxColumn { Name = "colIp", HeaderText = "IP Address", DataPropertyName = "Ip" });

            gridSvrIps.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "colEdit",
                HeaderText = "Action",
                Text = "Edit IP",
                UseColumnTextForButtonValue = true,
                Width = 80
            });
        }
        private async Task LoadAddressObjects()
        {
            try
            {
                var response = await client.GetAsync("/api/sonicos/address-objects/ipv4");
                if (!response.IsSuccessStatusCode) return;

                string json = await response.Content.ReadAsStringAsync();
                var list = new List<AddressObjectModel>();

                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    // Note: Adjust "address_objects" if your specific firmware version 
                    // uses a different root key, but this is standard for SonicOS.
                    var array = doc.RootElement.GetProperty("address_objects");

                    foreach (var item in array.EnumerateArray())
                    {
                        // 1. Only look at objects that have an "ipv4" key
                        if (item.TryGetProperty("ipv4", out var ipv4))
                        {
                            string name = ipv4.TryGetProperty("name", out var n) ? n.GetString() : "Unknown";
                            string zone = ipv4.TryGetProperty("zone", out var z) ? z.GetString() : "N/A";
                            string uuid = ipv4.TryGetProperty("uuid", out var u) ? u.GetString() : "";

                            // 2. Safely get the IP (Only Host objects have a "host" property)
                            string ip = "N/A";
                            if (ipv4.TryGetProperty("host", out var host))
                            {
                                ip = host.TryGetProperty("ip", out var ipVal) ? ipVal.GetString() : "N/A";
                            }
                            else if (ipv4.TryGetProperty("network", out var net)) // Handle Network objects
                            {
                                ip = net.TryGetProperty("ip", out var netIp) ? netIp.GetString() : "N/A";
                            }

                            list.Add(new AddressObjectModel
                            {
                                Name = name,
                                Zone = zone,
                                Ip = ip,
                                Uuid = uuid
                            });
                        }
                    }
                }
                gridSvrIps.DataSource = list;
            }
            catch (Exception ex) { MessageBox.Show("Load Error: " + ex.Message); }
        }
        private async void gridSvrIps_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || gridSvrIps.Columns[e.ColumnIndex].Name != "colEdit") return;

            var item = (AddressObjectModel)gridSvrIps.Rows[e.RowIndex].DataBoundItem;

            // Create Input Form
            using (Form f = new Form { Text = $"Edit {item.Name}", Size = new Size(300, 200), StartPosition = FormStartPosition.CenterParent })
            {
                TextBox txtIp = new TextBox { Left = 20, Top = 40, Width = 240, Text = item.Ip };
                Button btnSave = new Button { Text = "Save", Left = 20, Top = 80, DialogResult = DialogResult.OK };

                f.Controls.Add(new Label { Left = 20, Top = 20, Text = "New IP Address:" });
                f.Controls.Add(txtIp);
                f.Controls.Add(btnSave);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    // Validate the input before attempting the update
                    if (!IsValidIp(txtIp.Text))
                    {
                        MessageBox.Show("Invalid IP Address format. Please use a.b.c.d");
                        return; // Stop execution if the IP is bad
                    }

                    await UpdateAddressObject(item, txtIp.Text);
                    await LoadAddressObjects(); // Refresh Grid
                }
            }
        }

        private async Task UpdateAddressObject(AddressObjectModel item, string newIp)
        {
            // Exact structure from your Postman test
            var payload = new
            {
                address_object = new
                {
                    ipv4 = new
                    {
                        name = item.Name,
                        uuid = item.Uuid,
                        zone = item.Zone,
                        host = new { ip = newIp }
                    }
                }
            };

            string json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/sonicos/address-objects/ipv4/name/{item.Name}", content);

            if (response.IsSuccessStatusCode)
            {
                await CommitChanges(); // Reusing your existing Commit method
                MessageBox.Show("Updated successfully!");
            }
            else
            {
                MessageBox.Show("Failed to update: " + await response.Content.ReadAsStringAsync());
            }
        }

        private async void btnLoadSvrIp_Click(object sender, EventArgs e)
        {
            // 1. Prepare the grid columns first
            SetupSvrGrid();

            // 2. Call the async method to fetch and populate the data
            await LoadAddressObjects();
        }
    }
    }