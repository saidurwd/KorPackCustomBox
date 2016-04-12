﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Agent.aspx.cs" Inherits="LiveChat.ContactChat.Agent1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>KORPACK Live Chat</title>

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

        .chat-session {
            padding: 3px 3px 3px 3px;
            height: 65px;
            border-bottom: 1px solid #000;
            cursor: pointer;
        }

            .chat-session.active {
                background-color: lightgray;
                cursor: default;
            }

        .chat-msgs {
            height: 650px;
            padding: 3px 3px 3px 3px;
            border: 3px dashed lightgray;
            margin-bottom: 10px;
            overflow-y: auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="navbar navbar-inverse navbar-fixed-top">
                <div class="container">
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                            <span class="sr-only">Toggle navigation</span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                        <a class="navbar-brand" href="/ContactChat/Agent.html">KORPACK Live Chat - Agent Panel</a>
                    </div>
                    <div class="navbar-collapse collapse">
                        <ul class="nav navbar-nav">
                            <li><a href="#" id="show-real-time-visits">Show Real Time Visits</a></li>
                            <li><a href="#" id="show-internal-chat">Internal Agent Chat <span class="badge">...</span></a></li>
                        </ul>
                        <p class="navbar-text navbar-right">
                            Logged in as <a href="#" id="change-status" class="navbar-link"></a>
                        </p>
                    </div>
                    <!--/.nav-collapse -->
                </div>
            </div>

            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-3">
                        <div class="well sidebar-nav">
                            <h3>Chat Sessions</h3>
                            <div id="chat-sessions">
                            </div>
                        </div>
                        <!--/.well -->
                    </div>
                    <!--/span-->
                    <div id="chat-content" class="col-md-9">
                        <div id="login">
                            <h2>Log In to Start Accepting Chat Requests</h2>
                            <div id="login-alerts"></div>
                            <fieldset>
                                <legend>Enter your agent name and password</legend>
                                <p>
                                    <label>Agent Name</label>
                                    <input id="login-name" type="text" class="form-control" placeholder="agent name" />
                                </p>
                                <p>
                                    <label>Agent Password</label>
                                    <input id="login-pass" class="form-control" type="password" />
                                </p>
                                <br />
                                <button id="login-btn" type="button" class="btn btn-primary btn-medium">Start accepting chat ></button>
                            </fieldset>
                        </div>
                        <div id="agent-chat">
                            <div id="real-time-visits">
                                <h2>Real time visits</h2>
                                <table id="current-visits" class="table table-striped table-bordered">
                                    <thead>
                                        <tr>
                                            <th>Requested on</th>
                                            <th>Page</th>
                                            <th>Referrer</th>
                                            <th>City</th>
                                            <th>Country</th>
                                            <th>In Chat</th>
                                            <th>Invite</th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>
                            <div id="all-chatbox">
                                <div id="chatmsgsinternal" class="chat-msgs"></div>
                            </div>
                            <div id="chat-controls" class="row">
                                <div class="col-md-10">
                                    <input id="post-msg" type="text" class="form-control" placeholder="Enter your text here. Enter /list for a list of available commands" />
                                </div>
                                <div class="col-md-2">
                                    <button id="post-btn" class="btn btn-primary">Send</button>
                                </div>
                            </div>


                        </div>
                    </div>
                    <!--/span-->
                </div>
                <!--/row-->

            </div>
            <!--/.fluid-container-->

            <div id="modal-cmd" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="cmdLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h3>Available commands</h3>
                        </div>
                        <div class="modal-body">
                        </div>
                        <div class="modal-footer">
                            <button class="btn" data-dismiss="modal" aria-hidden="true">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        <script src="../Scripts/jquery-2.2.2.min.js" type="text/javascript" ></script>
        <script src="assets/js/bootstrap.min.js" type="text/javascript"></script>

        <script src="assets/js/jquery.timeago.js" type="text/javascript"></script>

        <script src="assets/js/jquery.signalR-2.1.1.min.js" type="text/javascript"></script>
        
        <script src="/signalr/hubs" type="text/javascript"></script>
        <script src="agent.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                setTimeout(function () {
                    $('#login-name').focus();
                }, 0);

                $('#login-pass').keypress(function (e) {
                    if (e.keyCode == 13)
                        $('#login-btn').click();
                });
            });
    </script>
    </form>
</body>
</html>
