$(document).ready(function () {
          /* jQuery("#dropmenu ul").css({ display: "none" }); // Opera Fix
            jQuery("#dropmenu li").hover(function () {
                jQuery(this).find('ul:first').css({ visibility: "visible", display: "none" }).show(268);
            }, function () {
                jQuery(this).find('ul:first').css({ visibility: "hidden" });
            });

            */



            //$('.showDateCss').datepicker({ showOn: 'both', buttonImage: '../../App_Themes/smoothness/images/calendar.gif', buttonImageOnly: true, dateFormat: 'dd/mm/yy' });


            //$(".customtextbox").htmlarea(); // Initialize all TextArea's as jHtmlArea's with default values

            $(".customtextbox").htmlarea(); // Initialize jHtmlArea's with all default values

            // Accordion
            if (navigator.appName.indexOf("Microsoft Internet Explorer") > (-1)) {
                //alert('browser is ie');
                $("#accordion").accordion({ header: "h3",
                    collapsible: true,
                    autoHeight: false  //for ie6 it should be true
                });
            }
            else {
                //alert(navigator.appName);        
                $("#accordion").accordion({ header: "h3",
                    collapsible: true,
                    autoHeight: false
                });
            }

            //ajaxSetup
            $.ajaxSetup({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: "{}",
                dataFilter: function (data) {
                    var msg;

                    if (typeof (JSON) !== 'undefined' &&
                    typeof (JSON.parse) === 'function')
                        msg = JSON.parse(data);
                    else
                        msg = eval('(' + data + ')');

                    if (msg.hasOwnProperty('d'))
                        return msg.d;
                    else
                        return msg;
                }
            });


            $('#tabsMain').tabs();
            $('.tabsAdmin').tabs();

            $("button, input:submit,input:button, .specialLink").button();

            $('.menuItemSettings').hover(function () {
                $('.subSettings').show();
            }

            );

            $('.subSettings').hover(function () { }, function () {
                $(this).hide();
            });

            //$("button, input:submit, .specialLink").live(function() {
            // $(this).button();
            //});


            var zIndexNumber = 1000;
            $('#ie7 div').each(function () {
                $(this).css('zIndex', zIndexNumber);
                zIndexNumber -= 10;
            });

            

            //=========================================================================================================================
            //==================================================== *For creating Rounded Corner  ===================================
            //=======================================================================================================================

            $('.roundedTableSmall').corners("10px 10px");

            //====================================================== END ====================================================================

        });


        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                $("button, input:submit, .specialLink").button();
                //makeClear();
            }


            $(".gridview tr").filter(function () {
                return $('td', this).length && !$('table', this).length
            }).css({ background: "ffffff" }).hover(
                function () { $(this).css({ background: "#C1DAD7" }); },
                function () { $(this).css({ background: "#ffffff" }); }
                );


            jQuery.each(jQuery.browser, function (i) {
                if ($.browser.msie) {
                    //alert(i);
                } else {
                    //alert(i);
                    if ((i == 'webkit') || ((i == 'safari'))) {
                        //alert('crome');
                        $('#ctl00_uProgress').hide(5000);
                    }
                }
            });

        }

        