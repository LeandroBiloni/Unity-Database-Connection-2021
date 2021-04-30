<?php
	
	//Tomo del form que viene de Unity los strings que guardE con los respectivos nombres
	$servename = $_POST["host"];
	$database = $_POST["database"];
	$srvr_user = $_POST["dbuser"];
	$srvr_pw = $_POST["dbpw"];

	//mysqli_connect() intenta abrir una conexion al servidor MySQL
	//En caso de tener exito, regresa un objeto representando la conexion a la base de datos. De no tener exito regresa FALSE
	$con = mysqli_connect($servename, $srvr_user, $srvr_pw, $database);

	//Chequeo si no hubo problema con la conexion
	if (mysqli_connect_errno())
	{
		echo("1: Connection failed"); //Si hubo, devuelvo un string
		exit(); //y salgo de la ejecucion
	}


	
	//Tomo los demas datos del form creado en Unity
	$query = $_POST["query"];

	/////$username = $_POST["user"];
	/////$query = "SELECT score FROM `highscores` WHERE user = ('".$username."');";

	//Ejecuto la sentencia y recibo el resultado en una variable
	$result = mysqli_query($con, $query);

	//Si el resultado de la query anterior no me devolvio ninguna linea (o sea que no existe el usuario en la tabla highscores)
	if (mysqli_num_rows($result) != 1)
	{
		//Devuelvo que el usuario no ingreso un score
		echo("User didn't uploaded a score");
		exit();
	}

	//Creo la variable donde voy a guardar el resultado
	$ret = "0";

	//Tomo el row -la fila- de todos los que me devolvio el select 
	//(en este caso es obvio que va a devolverme uno solo porque el usuario es unico)
	$row = mysqli_fetch_array($result);

	//Sumo a mi variable string de retorno el REGISTRO que hay en el CAMPO `score` de la linea tomada en row
	//(\n es un salto de linea, esto lo uso para luego parsear en Unity e ignorar el 0)
	$ret = $ret . "\n" . $row["score"];


//------------------------------------

	//$row = mysqli_fetch_array($result); Lo puedo utilizar en un while para ir obtiendo las filas en cada iteracion
	//Por ejemplo, si yo hubiera obtenido con mi select las filas con usuario y contrasenia, haria lo siguiente para devolverlo:
	//(utilizo \t para generar un espacio de tabulacion y poder parsear la fila luego de parsear el texto en Unity)

	//$ret = "0";

	//while($row = mysqli_fetch_array($result))
	//{
	//	$ret = $ret . "\n" .  $row["username"] . "\t" . $row["password"];
	//}
//------------------------------------


	echo($ret);
	
	//Cierro la conexion
	mysqli_close($con);

?>