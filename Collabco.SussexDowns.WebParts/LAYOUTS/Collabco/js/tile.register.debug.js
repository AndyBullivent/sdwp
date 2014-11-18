function spinRegisterTile(uid, linkURL) {
    startSpinning("register", uid);

    setTimeout(function () {
        setupRegisterTile(uid, linkURL);
    }, 3285);
}

function setupRegisterTile(uid, linkURL) {
    try {
        if (determinePageState() == "live") {
            var params = { dataType: "RegisterCount" };
            var soapMessage = generateSOAP("GetData", params); //Generate soap data for a request to the Saturn Endpoint

            var urlParts = document.createElement('a');
            urlParts.href = document.URL;
            var serviceURL = urlParts.protocol + "//" + urlParts.hostname + "/_layouts/15/collabco/services/myregistersendpoint.aspx";
            jQuery.ajax({
                type: "get",//"post", // Swap this over for the real request
                url: serviceURL,
                cache: false,
                dataType: "xml",
                data: soapMessage,
                contentType: "text/xml; charset=\"utf-8\"",
                timeout: 120000,
                error: function (request, error) {
                    makeTileError("register", uid);
                },
                complete: function () {
                    stopSpinning("register", uid);
                },
                beforeSend: function () {
                    startSpinning("register", uid);
                },
                success: function (data) {
                    if (jQuery(data).find('register').length !== 1) {
                        makeTileError("register", uid);
                    }
                    else {
                        var myNonRegisterCount = jQuery.trim(jQuery(data).find('MyNonRegisterCount').text());

                        var tileContents = "";
                        tileContents += "<div id='register'>";
                        tileContents += "    <div class='attenSlider_container'>";
                        tileContents += "       <a href='" + linkURL + "' target=_blank>";
                        tileContents += "        <h1 class='sliderTitle'><b>" + myNonRegisterCount + "</b></h1>";
                        tileContents += "       </a>";
                        tileContents += "    </div>";
                        tileContents += "</div>";

                        jQuery("#hubtile-register-" + uid + " .hubslider_container").html(tileContents);

                        makeTileDynamic("register", uid);
                    }
                }
            });
        }
    }
    catch (e) {
        console.log("tile.register > setupRegisterTile() :" + e);
        makeTileError("register", uid);
    }
}
