// Write your Javascript code.

$(document).ready(function(){
	
	function load_items_productos(){
		var item_str = "items";
		var form = $("#window_{0} form".format(item_str));

		function create_grid_items(){  
			if($("#grid_{0}".format(item_str)).length == 0) return;
			var grid;
			var columns = [
				{field: "TipoItem", title: "Tipo", template: "<span id='TipoItem'>#:TipoItem#</span>"},
				{field: "Descripcion", title: "Descripción"},
				{field: "MontoItem", title: "Monto", format: "{0:c}"},
				{field: "Moneda", title: "Moneda", template: "<span id='Moneda'>#:Moneda#</span>"},
				//{field: "Activo", title: "Activo"},
				{field:"ItemId", title:"Acciones", width:"100px", 
				template: "<a  id='btn_ver' href='/Items/Item/#:ItemId#'><i _id='#:ItemId#' id='btn_editar' class='fa fa-pencil-square' title='Editar'></i></a>"}
			];

			function data_bound_items(e){
				var get_tipo = function(id){
					return $.grep(tiposItem, function(item){
						return item.TipoItem == id;
					})[0].Nombre;
				}
				var get_tipo_moneda = function(id){
					return $.grep(tiposMoneda, function(item){
						return item.Id == id;
					})[0].Nombre;
				}

				$.each(e.sender.items(), function(index, item){
					item = $(item);
					//item.find("#btn_editar").on("click", edit_item);
					item.find("#TipoItem").text( get_tipo(item.find("#TipoItem").text()) );
					item.find("#Moneda").text( get_tipo_moneda(item.find("#Moneda").text()) );
				});
			}; 

			//INIT
			$.get('/Items/ObtenerItems/', function(response) {
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
				form.find("[name='ItemId']").val(id);
				
				var item = sender.parent().parent();
				//var data = $("#grid_"+item_str).data("kendoGrid").dataItem(item);
				console.log("data edit" , data);

				response = data;
				set_data_object_to_form("form", data);
				//$("#window_"+item_str).data("kendoWindow").center().open();
			}

			function clear_controls(){
				form.find("input[name]").val("");
				form.find("[name='ItemId']").val("0");
			}

			function register_item(){
				if(!validator.validate()) return false;

				var url = '/Items/GuardarItem';
				var data = get_data_object_from_form("form");
				var fn_compare_id = function(item, response){
						return item.productoId == response.productoId;
					}
				post_url(url, data, item_str, fn_compare_id);
				return false;
			}

			function load_catalogs(){
				form.find("[name='TipoItem']").kendoDropDownList({
	                dataTextField: "Nombre",
	                dataValueField: "TipoItem",
	                dataSource: tiposItem
	            }).data("kendoDropDownList");

	            form.find("[name='Moneda']").kendoDropDownList({
	                dataTextField: "Nombre",
	                dataValueField: "Id",
	                dataSource: tiposMoneda
	            }).data("kendoDropDownList");
			}

			if(get_id_from_url() != 0)
				$.get('/Items/ObtenerItem/'+get_id_from_url(), function(response) {
					edit_item({ currentTarget: $("<span></span>").attr("_id", get_id_from_url()) }, response);
				}, 'json');
			//window = create_window_controls(item_str, clear_controls);
			validator = load_form_controls(item_str, register_item);
			load_catalogs();
		}

		//INIT
		create_grid_items();
		create_edit_window();
	}

	//INIT MASTER
	load_items_productos();			
});




