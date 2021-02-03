// Write your Javascript code.
$('#confirm-delete').on('show.bs.modal', function (e) {
    $(this).find('.btn-ok').attr('href', $(e.relatedTarget).data('href'));
});


function getResources(resource, page = 1, deepClone = false) {
    var pageSize = 2;
    var statsHeader = resource + "s";
    if (statsHeader === "Countrys")
        statsHeader = "Countries";
    if (!deepClone && document.getElementById('statsHeader').innerHTML === statsHeader)
        return;

    $.get(
        "/Home",
        function (token) {
            $.ajaxSetup({
                headers: {
                    'Authorization': "Bearer " + token,
                    'Content-Type': "application/json",
                    'Access-Control-Allow-Origin': true
                }
            });

            $.get(
                "https://localhost:44340/Api/" + resource + "?page=" + page + "&pageSize=" + pageSize,
                function (data) {
                    
                    document.getElementById('statsHeader').innerHTML = statsHeader;

                    var titleString = 'title';
                    document.getElementById('titleString').innerHTML = "Title";
                    if (resource === "Player") {
                        titleString = 'nickname';
                        document.getElementById('titleString').innerHTML = "Nickname";
                    }
                    var oldBody = document.getElementById('statsBody');
                    var newBody = oldBody.cloneNode(deepClone);

                    //data = JSON.stringify(data);
                    //console.log(data);
                    //console.log(data.size);

                    var style = document.createAttribute("style");
                    style.value = "line-height: 40px; min-height: 40px; height: 40px";

                    var positionPlus = pageSize * (page - 1) + 1;
                    for (var i = 0; i < data.length; ++i) {
                        var tr = document.createElement('tr');
                        tr.setAttributeNode(style.cloneNode(true));

                        var tdPos = document.createElement('td');
                        var colspanPos = document.createAttribute("colspan");
                        colspanPos.value = 2;
                        tdPos.appendChild(document.createTextNode(i + positionPlus));
                        tdPos.setAttributeNode(colspanPos);
                        tr.appendChild(tdPos);

                        var tdTitle = document.createElement('td');
                        var colspanTitle = document.createAttribute("colspan");
                        colspanTitle.value = 7;
                        tdTitle.appendChild(document.createTextNode(data[i][titleString]));
                        tdTitle.setAttributeNode(colspanTitle);
                        tr.appendChild(tdTitle);

                        var tdScore = document.createElement('td');
                        var colspanScore = document.createAttribute("colspan");
                        colspanScore.value = 3;
                        tdScore.appendChild(document.createTextNode(data[i]['score']));
                        tdScore.setAttributeNode(colspanScore);
                        tr.appendChild(tdScore);

                        newBody.appendChild(tr);
                    }

                    oldBody.parentNode.replaceChild(newBody, oldBody);
                    //console.log('page content: ' + JSON.stringify(data));

                    if (data.length < pageSize)
                        document.getElementById("loadMoreButtonPlace").innerHTML = "";
                    else {
                        var funcCall = "getResources('" + resource + "'" + "," + (page + 1) + "," + true + ")";
                        //console.log(funcCall);
                        document.getElementById("loadMoreButtonPlace").innerHTML =
                            "<div class='col-md-2 col-md-offset-5'>" +
                            "<button class='btn btn-primary btn-block' onclick=" +  funcCall + ">Load more</button>" +
                            "</div>";
                    }
                }
            );
        }
    );
}