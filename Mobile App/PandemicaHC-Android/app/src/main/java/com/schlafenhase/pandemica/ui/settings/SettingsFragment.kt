package com.schlafenhase.pandemica.ui.settings

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProviders
import com.schlafenhase.pandemica.R
import com.google.firebase.auth.FirebaseAuth



class SettingsFragment : Fragment() {
    var fbAuth = FirebaseAuth.getInstance()

    private lateinit var settingsViewModel: SettingsViewModel

    override fun onCreateView(
            inflater: LayoutInflater,
            container: ViewGroup?,
            savedInstanceState: Bundle?
    ): View? {
        // Hide Action bar
        (activity as AppCompatActivity?)!!.supportActionBar!!.hide()

        // Initialize View Model
        settingsViewModel =
                ViewModelProviders.of(this).get(SettingsViewModel::class.java)
        val root = inflater.inflate(R.layout.fragment_settings, container, false)
        val textView: TextView = root.findViewById(R.id.text_hc_info)
        settingsViewModel.hcInfo.observe(viewLifecycleOwner, Observer {
            textView.text = it
        })

        // Map Button to sign out
        val btn = root.findViewById<View>(R.id.signOutButton) as Button
        btn.setOnClickListener {
            Toast.makeText(requireContext(), "Signing out...", Toast.LENGTH_LONG).show()
            fbAuth.signOut()
        }

        // Return to sign in screen
        fbAuth.addAuthStateListener {
            if(fbAuth.currentUser == null){
                activity?.finish()
            }
        }

        return root
    }
}
