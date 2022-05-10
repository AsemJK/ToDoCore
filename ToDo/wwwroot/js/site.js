//ToDo--------------------------------

function loadToDoListData(tenant) {
    var apifullurl = "/Api/ToDo?tenant=" + tenant;
    $('#datatable_ToDoList').DataTable({

        "ajax": {
            url: apifullurl,
            "type": "GET",
            "Headers": { 'id': '' },
            "dataSrc": function (response) {
                console.log(response);
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
                "render": function (data, type, full, meta) { return '<a class="btn btn-round btn-info btn-icon btn-sm like" onclick="return ViewToDoHistory(' + full.id + ');return false" title="View"><i class="fas fa-eye"></i></a><a class="btn btn-round btn-danger btn-icon btn-sm remove" href="#!" onclick="DeleteService(' + full.id + ');return false;" title="Delete"><i class="fas fa-times"></i></a>'; }
            }
        ]
    });
}

function ViewToDoHistory(todoid) {
    window.location.href = '/ToDo/' + todoid;
}

function CheckLogin(burl)
{
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
                console.log(response);
                sessionStorage.setItem("ulogin",response);
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
                if (sessionStorage.getItem("ulogin") == 'true')
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

    var fileUpload = $("#IssueImageFile").get(0);
    var files = fileUpload.files;
    fileData.append('TenantId', $("#dlToDoTenants").val());
    fileData.append('ToDoSubject', $("#ToDoSubject").val());
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
                console.log(response);
                newToDoId = response.id;
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