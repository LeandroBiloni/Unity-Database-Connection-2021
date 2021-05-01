<?php
    $servename = $_POST["host"];
	$database = $_POST["database"];
	$srvr_user = $_POST["dbuser"];
	$srvr_pw = $_POST["dbpw"];

	$con = mysqli_connect($servename, $srvr_user, $srvr_pw, $database);
	
	if (mysqli_connect_errno())
	{
		echo("1: Connection failed");
		exit();
	}

	$query = $_POST["query"];

	$result = mysqli_query($con, $query);

	if(mysqli_num_rows($result) != 1)
	{
		echo("No user found");
		exit();
	}

	

	mysqli_close($con);
?>