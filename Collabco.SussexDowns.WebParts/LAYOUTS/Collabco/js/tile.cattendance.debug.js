function spinCourseAttendanceTile(uid, exampleSetting) {
    startSpinning("cattendance", uid);

    setTimeout(function () {
        setupCourseAttendanceTile(uid);
    }, 3285);
}

function setupCourseAttendanceTile(uid) {
    try {
        if (determinePageState() == "live") {
            var params = { dataType: "CourseAttendancePercentage" };
            var soapMessage = generateSOAP("GetData", params); //Generate soap data for a request to the Saturn Endpoint

            var urlParts = document.createElement('a');
            urlParts.href = document.URL;

            //var serviceURL = urlParts.protocol + "//" + urlParts.hostname + "/_vti_bin/Saturn/SaturnService.asmx"; // Real saturn endpoint url. Must use POST request and parse the GetDataResult node in the return xml
            //var serviceURL = urlParts.protocol + "//" + urlParts.hostname + "/_layouts/15/collabco/js/testdata.xml"; // Fake saturn endpoint test data. Must use GET request and parse the attendance node in the return xml
            var serviceURL = urlParts.protocol + "//" + urlParts.hostname + "/_layouts/15/collabco/services/MyCourseAttendanceEndPoint.aspx";
            jQuery.ajax({
                type: "get",//"post", // Swap this over for the real request
                url: serviceURL,
                cache: false,
                dataType: "xml",
                data: soapMessage,
                contentType: "text/xml; charset=\"utf-8\"",
                timeout: 120000,
                error: function (request, error) {
                    makeTileError("cattendance", uid);
                },
                complete: function () {
                    stopSpinning("cattendance", uid);
                },
                beforeSend: function () {
                    startSpinning("cattendance", uid);
                },
                success: function (data) {
                    if (jQuery(data).find('CourseAttendance').length !== 1) {
                        alert(data);
                        makeTileError("cattendance", uid);
                    }
                    else {
                        ShowContent(uid, data);
                    }
                }
            });
        }
    }
    catch (e) {
        console.log("tile.cattendance > setupCourseAttendanceTile() :" + e);
        makeTileError("cattendance", uid);
    }
}
function ShowContent(uid, data) {
    

    // Build tile
    var CourseAttendanceAgg = jQuery.trim(jQuery(data).find('CourseAttendanceAgg').text());
    var tileContents = "";

    tileContents += "<div id='cattendance'>";
    tileContents += "    <div class='attenSlider_container'>";
    tileContents += "        <h5 class='sliderTitle'>This Academic Year</h5>";
    tileContents += "        <div class='attenSlider'>";
    tileContents += "             <div class='attenSliderFill2' style='width: " + CourseAttendanceAgg + "%'>&nbsp;</div>";
    tileContents += "        </div>";
    tileContents += "        <h5 class='percentage'>" + CourseAttendanceAgg + "%</h5>";
    tileContents += "    </div>";
    tileContents += "</div>";

    jQuery("#hubtile-cattendance-" + uid + " .hubslider_container").html(tileContents);

    //Build modal

    var CurrentMonthString = jQuery.trim(jQuery(data).find('CurrentMonthString').text());

    jQuery('.month-head-' + uid).find('.title').text(CurrentMonthString);

    var modalContent = "";

    var courses = jQuery(data).find('Course');

    if (courses.length > 0) {

        modalContent += "<table>";
        modalContent += "    <tr>";
        modalContent += "        <th>Course Title</th>";
        modalContent += "        <th>Group</th>";
        modalContent += "        <th>Attendance</th>";
        modalContent += "    </tr>";

        jQuery(courses).each(function (index, item) {
            try {
                var CourseTitle = jQuery.trim(jQuery(item).children("CourseTitle").text());
                var Group = jQuery.trim(jQuery(item).children("Group").text());
                var Attendance = jQuery.trim(jQuery(item).children("Attendance").text());


                modalContent += "    <tr>";
                modalContent += "        <td>" + CourseTitle + "</td>";
                modalContent += "        <td>" + Group + "</td>";
                modalContent += "        <td>" + Attendance + "%</td>";
                modalContent += "    </tr>";
            }
            catch (e) {
                console.log("tile.cattendance > setupAttendanceTile() Add course :" + e.message);
            }
        });

        modalContent += "</table>";
    }
    else {
        modalContent += "No courses!";
    }

    jQuery('#month-' + uid).append(modalContent);

    makeTileDynamic("cattendance", uid);
}