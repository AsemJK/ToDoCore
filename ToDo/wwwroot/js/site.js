//ToDo--------------------------------
var baseApiUrl = 'http://localhost:5121';
function loadToDoListData(apiKey,tenant) {
    $('#datatable_ToDoList').DataTable({

        "ajax": {
            url: baseApiUrl + "/ToDo?key=" + apiKey + "&tenant=" + tenant,
            "type": "GET",
            "Headers": { 'Id': '' },
            "dataSrc": function (response) {
                console.log(response);
                if (response.iTotalRecords == 0) {
                }
                return response.Items;
            }
        }
        ,
        columns: [
            { data: "Id" },
            { data: "toDoSubject" },
            { data: "teamMember" },
            { data: "creationDate" },
            { data: "lastStatus" },
            { data: "companyId" },
            {
                data: "Id",
                "render": function (data, type, full, meta) { return '<a class="btn btn-round btn-info btn-icon btn-sm like" onclick="return viewService(' + full.Id + ');return false" title="View"><i class="fas fa-eye"></i></a><a class="btn btn-round btn-danger btn-icon btn-sm remove" href="#!" onclick="DeleteService(' + full.Id + ');return false;" title="Delete"><i class="fas fa-times"></i></a>'; }
            }
        ]
    });
}