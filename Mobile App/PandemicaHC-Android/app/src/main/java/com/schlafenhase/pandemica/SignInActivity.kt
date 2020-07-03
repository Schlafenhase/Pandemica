package com.schlafenhase.pandemica

import android.content.Intent
import android.os.Bundle
import com.google.android.material.bottomnavigation.BottomNavigationView
import androidx.appcompat.app.AppCompatActivity
import androidx.navigation.findNavController
import androidx.navigation.ui.AppBarConfiguration
import androidx.navigation.ui.setupActionBarWithNavController
import androidx.navigation.ui.setupWithNavController
import android.view.View
import android.widget.Toast
import com.google.firebase.auth.AuthResult
import com.google.firebase.auth.FirebaseAuth
import com.google.android.gms.tasks.OnCompleteListener
import android.widget.TextView

class SignInActivity : AppCompatActivity() {
    var fbAuth = FirebaseAuth.getInstance()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_sign_in)
        supportActionBar?.hide()
    }

    override fun onStart() {
        super.onStart()
        // Check if user is signed in (non-null) and update UI accordingly.
        val currentUser = fbAuth.currentUser
        if (currentUser != null) {
            // Signed in, redirect to Main activity
            var intent = Intent(this, TabbedActivity::class.java)
            intent.putExtra("id", fbAuth.currentUser?.email)
            startActivity(intent)
        }
    }

    /**
     * Enters app main screen on login sucess
     */
    fun enterApp() {
        setContentView(R.layout.activity_tabbed)
        val navView: BottomNavigationView = findViewById(R.id.nav_view)

        val navController = findNavController(R.id.nav_host_fragment)
        // Passing each menu ID as a set of Ids because each
        // menu should be considered as top level destinations.
        val appBarConfiguration = AppBarConfiguration(setOf(
            R.id.navigation_patients, R.id.navigation_reports, R.id.navigation_settings))
        setupActionBarWithNavController(navController, appBarConfiguration)
        navView.setupWithNavController(navController)
    }

    /**
     * Attempts Firebase authentication and API info verification
     */
    fun signIn(email: String, password: String){
        Toast.makeText(
            baseContext, "Authenticating...",
            Toast.LENGTH_LONG
        ).show()

        fbAuth.signInWithEmailAndPassword(email, password)
            .addOnCompleteListener(this, OnCompleteListener<AuthResult> { task ->
            if (task.isSuccessful) {
                val user = fbAuth.currentUser
                enterApp()
            } else{
                Toast.makeText(
                    baseContext, "Error: ${task.exception?.message}",
                    Toast.LENGTH_LONG
                ).show()
            }
        })
    }

    fun signInClick(event: View) {
        val email = findViewById<TextView>(R.id.email_entry).text.toString()
        val password = findViewById<TextView>(R.id.password_entry).text.toString()
        if (email.isEmpty() && password.isEmpty()) {
            Toast.makeText(
                baseContext, "Email & password can't be empty.",
                Toast.LENGTH_LONG
            ).show()
            return
        } else if (email.isEmpty()) {
            Toast.makeText(
                baseContext, "Email can't be empty.",
                Toast.LENGTH_LONG
            ).show()
            return
        } else if (password.isEmpty()) {
            Toast.makeText(
                baseContext, "Password can't be empty.",
                Toast.LENGTH_LONG
            ).show()
            return
        }
        signIn(email, password)
    }
}
