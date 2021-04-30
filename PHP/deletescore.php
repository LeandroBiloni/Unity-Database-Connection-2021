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
	/////$queryCheck = $_POST["queryCheck"];
	$username = $_POST["user"];

	$queryCheck = "SELECT user FROM `highscores` WHERE user = ('".$username."');";

	//Ejecuto la sentencia y recibo el resultado en una variable
	$result = mysqli_query($con, $queryCheck);

	//Si el resultado de la query anterior me devolvio alguna linea
	if (mysqli_num_rows($result) > 0)
	{
		//Elimino el usuario de la tabla
		/////$queryDelete = $_POST["queryDelete"];
		
		$queryDelete = "DELETE FROM highscores WHERE user = ('" . $username . "');";

		mysqli_query($con, $queryDelete) or die("2: Delete error");
	}

	echo("0");
	
	//Cierro la conexion
	mysqli_close($con);

?>