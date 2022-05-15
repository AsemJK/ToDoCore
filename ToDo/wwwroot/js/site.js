//Common functions

function ActionConfirm() {
    var r = false;
    r = confirm("Are you sure ?!");
    if (r == true) {
        return true;
    } else {
        return false;
    }
}

//ToDo--------------------------------

function loadToDoListData(tenant, qst) {
    if ($.fn.DataTable.isDataTable("#datatable_ToDoList"))
        $('#datatable_ToDoList').DataTable().clear().destroy();

    var apifullurl = "/Api/ToDo?q=" + qst +"&tenant=" + tenant;
    $('#datatable_ToDoList').DataTable({
        "ajax": {
            url: apifullurl,
            "type": "GET",
            "Headers": { 'id': '' },
            "dataSrc": function (response) {
                if (response.iTotalRecords == 0) {
                }
                return response;
            }
        }
        ,
        columns: [
            { data: "id" },
            { data: "toDoSubject" },
            { data: "teamMember" },
            { data: "creationDate" },
            { data: "lastStatus" },
            { data: "companyId" },
            {
                data: "id",
                "render": function (data, type, full, meta) { return '<div style="display: flex"><a class="btn btn-round btn-info btn-icon btn-sm like mr-2" href="#!" onclick="return ViewToDoHistory(' + full.id + ');return false" title="View"><i class="fas fa-eye"></i></a><a class="btn btn-round btn-success btn-icon btn-sm ok mr-2" href="#!" onclick="ToDoDone(' + full.id + ');return false;" title="Make Done"><i class="fas fa-check"></i></a></div>'; }
            }
        ]
    });
}

function ToDoDone(id) {
    if (ActionConfirm()) {
        var formData = new FormData();
        formData.append('TicketId', id);
        formData.append('ToDoNewStatus', 'Done');

        $.ajax({
            type: "POST",
            url: '/api/ToDo/UpdateToDo',
            data: formData,
            processData: false,
            contentType: false,
            beforeSend: function () {
                $("#loader").show();
            },
            success: function (response) {

            },
            failure: function (response) {
                console.log(response.responseText);
            },
            error: function (response) {
                console.log(response.responseText);
            },
            complete: function (data) {
                // Hide image container
                $("#loader").hide();
                window.location.href = "/ToDo/" + id;
            }
        });
    }
}

function ViewToDoHistory(todoid) {
    sessionStorage.setItem('tickId', todoid);
    window.location.href = '/ToDo/' + todoid;
}

function CheckLogin(burl) {
    var vusername = $('#txtUserName').val();
    var vpassword = $('#txtPassword').val();
    if (vusername != null) {
        var formData = new FormData();
        formData.append('UserName', vusername);
        formData.append('Password', vpassword);

        $.ajax({
            type: "POST",
            url: burl,
            data: formData,
            processData: false,
            contentType: false,
            beforeSend: function () {
                $("#loader").show();
            },
            success: function (response) {
                sessionStorage.setItem("ulogin", response.UserName);
            },
            failure: function (response) {
                console.log(response.responseText);
            },
            error: function (response) {
                console.log(response.responseText);
            },
            complete: function (data) {
                // Hide image container
                $("#loader").hide();
                if (sessionStorage.getItem("ulogin") != '')
                    window.location.href = "/Home/Index";
            }
        });
    }
    else {
        $('#divAlertMessage').html('لا يمكن إتمام العملية');
        $('#divAlertMessage').show();
        setTimeout(function myfunction() {
            $('#divAlertMessage').hide();
        }, 3000);
    }

}

$('#btnAddNewToDo').click(function () {
    var newToDoId = 0;
    var fileData = new FormData();
    var ticketSubject = CKEDITOR.instances['ToDoSubject'].getData();
    var fileUpload = $("#IssueImageFile").get(0);
    var files = fileUpload.files;
    fileData.append('TenantId', $("#dlToDoTenants").val());
    fileData.append('ToDoSubject', ticketSubject);
    fileData.append('file-1', files[0]);

    if ($("#ToDoSubject").val() != '') {

        $.ajax({
            type: "Post",
            url: "/api/ToDo/AddToDo",
            data: fileData,
            processData: false,
            contentType: false,
            beforeSend: function () {
                $("#loader").show();
            },
            success: function (response) {
                newToDoId = response.id;
            },
            failure: function (response) {
                console.log(response.responseText);
            },
            error: function (response) {
                console.log(response.responseText);
            },
            complete: function (data) {
                $("#loader").hide();
                if (newToDoId > 0)
                    window.location.href = '/Home/Index';
            }
        });
    }
    else {
        $('#divAlertMessage').html('لا يمكن إتمام العملية');
        $('#divAlertMessage').show();
        setTimeout(function myfunction() {
            $('#divAlertMessage').hide();
        }, 3000);
    }
});

$('#btnSaveTicketComment').click(function () {
    var toDoId = sessionStorage.getItem('tickId');
    var fileData = new FormData();
    var newToDoDetailId = 0;
    fileData.append('TicketId', toDoId);
    var commentBody = CKEDITOR.instances['toDoNewComment'].getData();
    fileData.append('ToDoNewComment', commentBody);
    if (commentBody != '') {
        $.ajax({
            type: "Post",
            url: "/api/ToDo/AddToDoComment",
            data: fileData,
            processData: false,
            contentType: false,
            beforeSend: function () {
                $("#loader").show();
            },
            success: function (response) {                
                newToDoDetailId = response.id;
            },
            failure: function (response) {
                console.log(response.responseText);
            },
            error: function (response) {
                console.log(response.responseText);
            },
            complete: function (data) {
                $("#loader").hide();
                if (newToDoDetailId > 0)
                    window.location.href = '/ToDo/' + toDoId;
            }
        });
    }
    else {
        $('#divAlertMessage').html('لا يمكن إتمام العملية');
        $('#divAlertMessage').show();
        setTimeout(function myfunction() {
            $('#divAlertMessage').hide();
        }, 3000);
    }
});


function loadTicketHistoryListData(ticketId) {
    var apifullurl = "/Api/ToDo/ToDoHistory?ticketNo=" + ticketId;
    var html = '';
    if (ticketId  > 0) {
        $.ajax({
            type: "Get",
            url: apifullurl,
            data: null,
            processData: false,
            contentType: false,
            beforeSend: function () {
                $("#loader").show();
            },
            success: function (response) {
                response.forEach(function (value, key) {
                    html += '<div class="card" style="width: 100%;">';
                    html += '<div class="card-body">';
                    html += '<h5 class="card-title">' + value.teamMember + ' - ' + value.extraDate + '</h5>';
                    html += '<p class="card-text">' + value.notes +'</p>';
                    html += '<a href="#" class="btn btn-primary">Reply</a>';
                    html += '</div>';
                    html += '<div>';
                });
            },
            failure: function (response) {
                console.log(response.responseText);
            },
            error: function (response) {
                console.log(response.responseText);
            },
            complete: function (data) {
                $("#loader").hide();
                $('#divListOfTicketHistory').html(html);
            }
        });
    }
    else {
        $('#divAlertMessage').html('لا يمكن إتمام العملية');
        $('#divAlertMessage').show();
        setTimeout(function myfunction() {
            $('#divAlertMessage').hide();
        }, 3000);
    }
}
