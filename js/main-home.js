$.ajax({
    type: "POST",
    url: 'Home/GetData',
/*    data: 'data1=' ,*/
    /*                dataType: "json",*/
    success: function (data) {
        // alert(JSON.stringify(data)); show entire object in JSON format
        $.each(data, function (i, obj) {
          /*  alert(obj.DivisionId);*/
            console.log(obj.DivisionId);
        });
    },
});