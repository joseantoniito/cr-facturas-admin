﻿// Write your Javascript code.

$(document).ready(function(){
	
	function load_items_productos(){
		var item_str = "productos";
		var form = $("#window_{0} form".format(item_str));

		////ProductoId, EmpresaId, Nombre, Codigo, CodigoBarras, ProveedorId, TipoProductoId, 
		//CategoriaId, SubCategoriaId, PrecioCompra, PrecioFinal, Existencias, Impuesto, VistoEnFacturacion					
		function create_grid_items(){  
			if($("#grid_{0}".format(item_str)).length == 0) return;
			var grid;
			var columns = [
				{field: "Nombre", title: "Nombre"},
				{field: "Codigo", title: "Código"},
				{field: "CodigoBarras", title: "Código de Barras"},
				{field: "ProveedorId", title: "Proveedor"},
				{field: "TipoProductoId", title: "Tipo de Producto"},
				{field: "CategoriaId", title: "Categoría"},
				{field: "SubCategoriaId", title: "Sub Categoría"},
				{field: "PrecioCompra", title: "Precio de Compra"},
				//{field: "PrecioFinal", title: "Precio Final"},
				//{field: "Existencias", title: "Existencias"},
				//{field: "Impuesto", title: "Impuesto"},
				//{field: "VistoEnFacturacion", title: "Visto En Facturacion"},
				{field:"ProductoId", title:"Acciones", width:"100px", 
				template: "<a  id='btn_ver' href='/Productos/Item/#:ProductoId#'><i _id='#:ProductoId#' id='btn_editar' class='fa fa-pencil-square' title='Editar'></i></a>"}
			];

			function data_bound_items(e){
				$.each(e.sender.items(), function(index, item){
					item = $(item);
					//item.find("#btn_editar").on("click", edit_item);
				});
			}; 

			//INIT
			$.get('/Productos/ObtenerProductos/', function(response) {
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
				form.find("[name='productoId']").val(id);
				
				var item = sender.parent().parent();
				//var data = $("#grid_"+item_str).data("kendoGrid").dataItem(item);
				console.log("data edit" , data);

				response = data;
				set_data_object_to_form("form", data);
				//$("#window_"+item_str).data("kendoWindow").center().open();
			}

			function clear_controls(){
				form.find("input[name]").val("");
				form.find("[name='productoId']").val("0");
			}

			function register_item(){
				if(!validator.validate()) return false;

				var url = '/Productos/GuardarProducto';
				var data = get_data_object_from_form("form");
				var fn_compare_id = function(item, response){
						return item.productoId == response.productoId;
					}
				post_url(url, data, item_str, fn_compare_id);
				return false;
			}

			if(get_id_from_url() != 0)
				$.get('/Productos/ObtenerProducto/'+get_id_from_url(), function(response) {
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
	load_items_productos();			
});




