function spinStudentsTile(uid, linkURL) {
    startSpinning("students", uid);

    setTimeout(function () {
        setupStudentsTile(uid, linkURL);
    }, 3285);
}

function setupStudentsTile(uid, linkURL) {
    try {
        if (determinePageState() == "live") {
            var params = { dataType: "OpenConcerns" };
            var soapMessage = generateSOAP("GetData", params); //Generate soap data for a request to the Saturn Endpoint

            var urlParts = document.createElement('a');
            urlParts.href = document.URL;
            var serviceURL = urlParts.protocol + "//" + urlParts.hostname + "/_layouts/15/collabco/services/mystudentsendpoint.aspx";
            jQuery.ajax({
                type: "get",//"post", // Swap this over for the real request
                url: serviceURL,
                cache: false,
                dataType: "xml",
                data: soapMessage,
                contentType: "text/xml; charset=\"utf-8\"",
                timeout: 120000,
                error: function (request, error) {
                    makeTileError("students", uid);
                },
                complete: function () {
                    stopSpinning("students", uid);
                },
                beforeSend: function () {
                    startSpinning("students", uid);
                },
                success: function (data) {
                    if (jQuery(data).find('students').length !== 1) {
                        makeTileError("students", uid);
                    }
                    else {
                        var myOpenConcerns = jQuery.trim(jQuery(data).find('OpenConcerns').text());

                        var tileContents = "";

                        tileContents += "<div id='students'>";
                        tileContents += "    <div class='attenSlider_container'>";
                        tileContents += "       <a href='" + linkURL + "' target=_blank>";
                        tileContents += "        <h1 class='sliderTitle'><b>" + myOpenConcerns + "</b></h1>Open Concerns";
                        tileContents += "       </a>";
                        tileContents += "    </div>";
                        tileContents += "    <p>Open Concerns<p/>";
                        tileContents += "</div>";

                        jQuery("#hubtile-students-" + uid + " .hubslider_container").html(tileContents);
                        

                        makeTileDynamic("students", uid);
                    }
                }
            });
        }
    }
    catch (e) {
        console.log("tile.students > setupStudentsTile() :" + e);
        makeTileError("students", uid);
    }
}
