<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Install.aspx.cs" Inherits="LiveChat.ContactChat.Install" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Configure KORPACK Live Chat</title>
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/css/bootstrap-theme.min.css" rel="stylesheet" />
    <style type="text/css">
        body {
            padding-top: 60px;
            padding-bottom: 40px;
        }

        .sidebar-nav {
            padding: 9px 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="navbar navbar-inverse navbar-fixed-top">
                <div class="container">
                    <a class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </a>
                    <a class="navbar-brand" href="/ContactChat/agent.html">KORPACK Live Chat</a>
                    <div class="nav-collapse collapse">
                        <ul class="navbar-nav"></ul>
                    </div>
                    <!--/.nav-collapse -->
                </div>
            </div>

            <div class="container-fluid">
                <div class="container">
                    <div class="row">
                        <div class="col-md-12">
                            <div id="config">
                                <div class="jumbotron">
                                    <h2>Configuring KORPACK live chat</h2>
                                    <p>
                                        You can configure some basics information here. After the first install you will be prompt to
                                login to modify config values on this page. 
                           
                                    </p>
                                </div>

                                <div id="save-alerts"></div>

                                <h3>How agents can connect</h3>
                                <p>
                                    You have two main passwords that are saved in an encrypted way on the KORPACK Live chat.
                           
                                </p>

                                <fieldset>
                                    <legend>Set your passwords</legend>
                                    <p>
                                        <label>Admin password</label>
                                        <input type="password" id="main-pass" class="form-control" />
                                        <span class="help-block">This is for administrative access, all admins shares the same password.</span>
                                    </p>
                                    <p>
                                        <label>Agent password</label>
                                        <input type="password" id="agent-pass" class="form-control" />
                                        <span class="help-block">This is for your agents. All agents shares the same password.</span>
                                    </p>
                                    <p>
                                        <label>Email when no agents are online</label>
                                        <input type="email" id="email" class="form-control" placeholder="Email for offline contact" />
                                        <span class="help-block">When a visitor contact you when all agents are offline.</span>
                                    </p>
                                </fieldset>

                                <hr />

                                <a href="#" id="save-button" class="btn btn-primary btn-large">Save Changes</a>
                            </div>

                            <div id="login">
                                <h2>Log In to configure your installation</h2>
                                <div id="login-alerts"></div>
                                <fieldset>
                                    <legend>Enter the admin password</legend>
                                    <p>
                                        <label>Admin Password</label>
                                        <input id="login-pass" type="password" class="form-control" />
                                    </p>
                                    <button id="login-btn" type="button" class="btn btn-primary btn-medium">Login</button>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                    <!--/row-->


                </div>

                <footer>
                   
                </footer>
                <!--/.fluid-container-->
            </div>
        </div>
    </form>
   
      <script src="../Scripts/jquery-2.2.2.min.js" type="text/javascript" ></script>
    <script src="assets/js/bootstrap.min.js" type="text/javascript"></script>

    <script src="assets/js/jquery.timeago.js" type="text/javascript"></script>

    <script src="assets/js/jquery.signalR-2.1.1.min.js" type="text/javascript"></script>
    <script src="/signalr/hubs" type="text/javascript"></script>
    <script src="install.js" type="text/javascript"></script>
</body>
</html>
