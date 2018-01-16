// Write your Javascript code.

$(document).ready(function(){
	
	function load_items_clientes(){
		var item_str = "clientes";
		var form = $("#window_{0} form".format(item_str));

		function create_grid_items(){  
			if($("#grid_{0}".format(item_str)).length == 0) return;
			var grid;
			//ClienteId, EmpresaId, Nombre, Apellido1, Apellido2, Telefono1, Telefono2, Telefono3, 
			//Correo, Cumpleaños, RecomendadoPorClienteId, Status, CreditoDisponible
			var columns = [
				{field: "Nombre", title: "Nombre"},
				{field: "Apellido1", title: "Apellido 1"},
				{field: "Apellido2", title: "Apellido 2"},
				{field: "Telefono1", title: "Teléfono 1"},
				//{field: "Telefono2", title: "Teléfono 2"},
				//{field: "Telefono3", title: "Teléfono 3"},
				{field: "Correo", title: "Correo"},
				//{field: "Cumpleaños", title: "Cumpleaños"},
				//{field: "Horario", title: "Horario"},
				//{field: "TipoPago", title: "TipoPago"},
				//{field: "RecomendadoPorClienteId", title: "Recomendado Por Cliente"},
				{field: "Status", title: "Status"},
				{field: "CreditoDisponible", title: "Credito Disponible"},
				{field:"ClienteId", title:"Acciones", width:"100px", 
				template: "<a  id='btn_ver' href='/Clientes/Item/#:ClienteId#'><i _id='#:ClienteId#' id='btn_editar' class='fa fa-pencil-square' title='Editar'></i></a>"}
			];

			function data_bound_items(e){
				$.each(e.sender.items(), function(index, item){
					item = $(item);
					//item.find("#btn_editar").on("click", edit_item);
				});
			}; 

			//INIT
			jQuery("#btn_add_{0}".format(item_str)).attr("href", "/Clientes/Item/");
			$.get('/Clientes/ObtenerClientes/', function(response) {
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
				form.find("[name='clienteId']").val(id);
				
				var item = sender.parent().parent();
				//var data = $("#grid_"+item_str).data("kendoGrid").dataItem(item);
				console.log("data edit" , data);

				response = data;
				set_data_object_to_form("form", data);

				//$("#window_"+item_str).data("kendoWindow").center().open();
			}

			function clear_controls(){
				form.find("input[name]").val("");
				form.find("[name='clienteId']").val("0");
			}

			function register_item(){
				if(!validator.validate()) return false;

				var url = '/Clientes/GuardarCliente';
				var data = get_data_object_from_form("form");
				var fn_compare_id = function(item, response){
						return item.ClienteId == response.ClienteId;
					}
				post_url(url, data, item_str, fn_compare_id);
				return false;
			}

			if(get_id_from_url() != 0)
				$.get('/Clientes/ObtenerCliente/'+get_id_from_url(), function(response) {
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
	load_items_clientes();			
});




