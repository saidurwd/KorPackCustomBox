/// <reference path="../Scripts/jquery-1.3.2-vsdoc.js" />


        $(function() {

            $('#tabsfa').tabs();


            $('#btnSearch').click(function() {

                var txtSearchValue = $('#txtSearch').val();
                if (txtSearchValue.length == 0) {
                    alert('Search text can not be Empty');
                    return false;
                }
                else {
                    return true;
                }

            });



            //Showing Advance Search options
            $('.showAdvanace').hover(function() {

                $(this).addClass('advanceSearchHoverButton');
            },

      function() {
          $(this).removeClass('advanceSearchHoverButton');
      }
      );



            $('.showAdvanace').click(function() {
                $('.advanceSearchOption').slideToggle('slow')

            });



            //For Action button      
            $('.ui-action-button').hover(
             function() { $(this).addClass('ui-state-hover'); },
             function() {
                 $(this).removeClass('ui-state-hover');
             });



            //create corner for Tables
            $('.mainContent').corners("10px 10px");



        });






        function onUpdating() {
            // get the update progress div

            var pnlPopup = $get('pnlPopup');
            //jQuery('.cssTableSearch').css('background-color','Gray');

            //  get the gridview element        
            var gridView = $get('GridView1');
            var panelContainGridView = $get('Panel1_GridView1');

            // make it visible
            pnlPopup.style.display = 'inline';


            // get the bounds of both the gridview and the progress div
            var gridViewBounds = Sys.UI.DomElement.getBounds(gridView);
            var pnlPopupBounds = Sys.UI.DomElement.getBounds(pnlPopup);

            //  center of gridview
            var x = gridViewBounds.x + Math.round(gridViewBounds.width / 2) - Math.round(pnlPopupBounds.width / 2);
            // var y = gridViewBounds.y + Math.round(gridViewBounds.height / 2) - Math.round(pnlPopupBounds.height / 2); 
            var y = gridViewBounds.y;
            //alert(gridViewBounds.y);       
            //var y=216;

            //    set the progress element to this position
            Sys.UI.DomElement.setLocation(pnlPopup, x, y);
            //Sys.UI.DomElement.setLocation(pnlPopup); 
        }

        function onUpdated() {
            // get the update progress div
            var pnlPopup = $get('pnlPopup');
            // make it invisible
            pnlPopup.style.display = 'none';
        }  

       