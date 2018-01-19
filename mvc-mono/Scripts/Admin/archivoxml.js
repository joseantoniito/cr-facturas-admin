// Write your Javascript code.

$(document).ready(function(){
	
	function load_items_empleados(){
		var item_str = "empleados";
		var form = $("#window_{0} form".format(item_str));

		function create_grid_items(){  
			if($("#grid_{0}".format(item_str)).length == 0) return;
			var grid;
			var columns = [
				{field: "Nombre", title: "Nombre"},
				{field: "Apellido1", title: "Apellido 1"},
				{field: "Apellido2", title: "Apellido 2"},
				{field: "Telefono1", title: "Teléfono 1"},
				{field: "Telefono2", title: "Teléfono 2"},
				//{field: "Identificacion", title: "Identificacion"},
				//{field: "Foto", title: "Foto"},
				//{field: "Horario", title: "Horario"},
				//{field: "TipoPago", title: "TipoPago"},
				{field: "Puesto", title: "Puesto"},
				{field: "Status", title: "Status"},
				{field:"EmpleadoId", title:"Acciones", width:"100px", 
				template: "<a  id='btn_ver' href='/Personal/Miembro/#:EmpleadoId#'><i _id='#:EmpleadoId#' id='btn_editar' class='fa fa-pencil-square' title='Editar'></i></a>"}
			];

			function data_bound_items(e){
				$.each(e.sender.items(), function(index, item){
					item = $(item);
					//item.find("#btn_editar").on("click", edit_item);
				});
			}; 

			//INIT
			$("#btn_add_{0}".format(item_str)).attr("href", "/Personal/Miembro/");
			$.get('/Personal/ObtenerEmpleados/', function(response) {
				grid = create_grid(item_str, response, columns, data_bound_items);
			}, 'json');

		}

		function create_edit_window(){
			if(!$("#window_"+item_str).is(":visible")) return;
			var window, validator;

			function edit_item(event, data){
				console.log("entro a editar");
				var sender = $(event.currentTarget);
				var id = sender.attr("_id");
				form.find("[name='EmpleadoId']").val(id);
				
				var item = sender.parent().parent();
				//var data = $("#grid_"+item_str).data("kendoGrid").dataItem(item);
				console.log("data edit" , data);

				response = data;
				form.find("[name='EmpleadoId']").val(response.EmpleadoId);
				form.find("[name='EmpresaId']").val(response.EmpresaId);
				form.find("[name='Nombre']").val(response.Nombre);
				form.find("[name='Apellido1']").val(response.Apellido1);
				form.find("[name='Apellido2']").val(response.Apellido2);
				form.find("[name='Telefono1']").val(response.Telefono1);
				form.find("[name='Telefono2']").val(response.Telefono2);
				form.find("[name='Apodo']").val(response.Apodo);
				form.find("[name='Identificacion']").val(response.Identificacion);
				form.find("[name='Foto']").val(response.Foto);
				form.find("[name='Horario']").val(response.Horario);
				form.find("[name='TipoPago']").val(response.TipoPago);
				form.find("[name='Puesto']").val(response.Puesto);
				form.find("[name='Status']").val(response.Status);

				//$("#window_"+item_str).data("kendoWindow").center().open();
			}

			function clear_controls(){
				form.find("[name='EmpleadoId']").val("0");
				form.find("[name='EmpresaId']").val("");
				form.find("[name='Nombre']").val(""); 
				form.find("[name='Apellido1']").val("");
				form.find("[name='Apellido2']").val("");
				form.find("[name='Telefono1']").val("");
				form.find("[name='Telefono2']").val("");
				form.find("[name='Apodo']").val("");
				form.find("[name='Identificacion']").val("");
				form.find("[name='Foto']").val("");
				form.find("[name='Horario']").val("");
				form.find("[name='TipoPago']").val("");
				form.find("[name='Puesto']").val("");
				form.find("[name='Status']").val("");
			}

			function register_item(){
				if(!validator.validate()) return false;

				var url = '/Personal/GuardarEmpleado';
				var data = {
					EmpleadoId : form.find("[name='EmpleadoId']").val(),
					EmpresaId : form.find("[name='EmpresaId']").val(), 
					Nombre : form.find("[name='Nombre']").val(), 
					Apellido1 : form.find("[name='Apellido1']").val(),
					Apellido2 : form.find("[name='Apellido2']").val(), 
					Telefono1 : form.find("[name='Telefono1']").val(), 
					Telefono2 : form.find("[name='Telefono2']").val(), 
					Apodo : form.find("[name='Apodo']").val(), 
					Identificacion : form.find("[name='Identificacion']").val(), 
					Foto : form.find("[name='Foto']").val(), 
					Horario : form.find("[name='Horario']").val(), 
					TipoPago : form.find("[name='TipoPago']").val(),
					Puesto : form.find("[name='Puesto']").val(), 
					Status : form.find("[name='Status']").val(),  
				};
				var fn_compare_id = function(item, response){
						return item.EmpleadoId == response.EmpleadoId;
					}
				post_url(url, data, item_str, fn_compare_id);
				return false;
			}

			if(get_id_from_url() != 0)
				$.get('/Personal/ObtenerEmpleado/'+get_id_from_url(), function(response) {
					edit_item({ currentTarget: $("<span></span>").attr("_id", get_id_from_url()) }, response);
				}, 'json');
						
			//window = create_window_controls(item_str, clear_controls);
			validator = load_form_controls(item_str, register_item)
		}

		//INIT
		create_grid_items();
		create_edit_window();
	}

	//INIT MASTER
	load_items_empleados();			
});




