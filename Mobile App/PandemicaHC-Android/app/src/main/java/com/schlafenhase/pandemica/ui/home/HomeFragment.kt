package com.schlafenhase.pandemica.ui.home

import android.app.Activity
import android.content.Intent
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProvider
import androidx.navigation.fragment.NavHostFragment
import androidx.recyclerview.widget.ItemTouchHelper
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.google.android.material.floatingactionbutton.FloatingActionButton
import com.google.gson.Gson
import com.schlafenhase.pandemica.NewPatientActivity
import com.schlafenhase.pandemica.R
import com.schlafenhase.pandemica.data.Patient


class HomeFragment : Fragment() {

    private lateinit var homeViewModel: HomeViewModel
    private val newPatientActivityRequestCode = 1

    var itemTouchHelperCallback: ItemTouchHelper.SimpleCallback =
        object : ItemTouchHelper.SimpleCallback(0, ItemTouchHelper.RIGHT) {
            override fun onMove(
                recyclerView: RecyclerView,
                viewHolder: RecyclerView.ViewHolder,
                target: RecyclerView.ViewHolder
            ): Boolean {
                return false
            }

            override fun onSwiped(viewHolder: RecyclerView.ViewHolder, direction: Int) {
                homeViewModel.delete(viewHolder.adapterPosition)
            }
        }

    override fun onCreateView(
            inflater: LayoutInflater,
            container: ViewGroup?,
            savedInstanceState: Bundle?
    ): View? {
        val root = inflater.inflate(R.layout.fragment_home, container, false)

        // Show action bar
        (activity as AppCompatActivity?)!!.supportActionBar!!.show()

        // Create RecyclerView to see patients
        val recyclerView = root.findViewById<RecyclerView>(R.id.recyclerview)
        val adapter = PatientListAdapter(this.requireContext())
        recyclerView.adapter = adapter
        recyclerView.layoutManager = LinearLayoutManager(this.requireContext())

        // Attach helper to handle deleting
        val helper = ItemTouchHelper(itemTouchHelperCallback)
        helper.attachToRecyclerView(recyclerView)

        // Initialize Model
        homeViewModel = ViewModelProvider(this).get(HomeViewModel::class.java)
        homeViewModel.allPatients.observe(viewLifecycleOwner, Observer { patients ->
            // Update the cached copy of the words in the adapter.
            patients?.let { adapter.setPatients(it) }
        })

        // Initialize "add" floating action button
        val fab = root.findViewById<FloatingActionButton>(R.id.addFab)
        fab.setOnClickListener {
            val intent = Intent(this.context, NewPatientActivity::class.java)
            startActivityForResult(intent, newPatientActivityRequestCode)
        }

        return root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        view.findViewById<View>(R.id.button_home).setOnClickListener {
            val action = HomeFragmentDirections
                    .actionHomeFragmentToHomeSecondFragment("From HomeFragment")
            NavHostFragment.findNavController(this@HomeFragment)
                    .navigate(action)
        }
    }

    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)

        if (requestCode == newPatientActivityRequestCode && resultCode == Activity.RESULT_OK) {
            // Code is valid. Insert incoming patient in database
            data?.getStringExtra(NewPatientActivity.EXTRA_REPLY)?.let {
                val gson = Gson()
                val patient = gson.fromJson(it, Patient::class.java)
                homeViewModel.insertPatient(patient)
            }

            // Tell user that patient was saved
            Toast.makeText(
                context?.applicationContext,
                "Patient saved",
                Toast.LENGTH_LONG).show()
        } else {
            // Codes is invalid. Tell user patient was not saved
            Toast.makeText(
                context?.applicationContext,
                R.string.empty_not_saved,
                Toast.LENGTH_LONG).show()
        }
    }
}
