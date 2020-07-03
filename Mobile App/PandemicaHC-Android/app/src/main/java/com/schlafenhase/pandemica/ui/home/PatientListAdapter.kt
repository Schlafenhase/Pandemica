package com.schlafenhase.pandemica.ui.home

import android.annotation.SuppressLint
import android.content.Context
import android.content.Intent
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.google.gson.Gson
import com.schlafenhase.pandemica.EditPatientActivity
import com.schlafenhase.pandemica.R
import com.schlafenhase.pandemica.data.Patient
import com.schlafenhase.pandemica.util.ExpandableLayout
import kotlinx.android.synthetic.main.recyclerview_item.view.*


class PatientListAdapter internal constructor(
    context: Context
) : RecyclerView.Adapter<PatientListAdapter.PatientViewHolder>() {

    // Save the expanded row position
    private val expandedPositionSet: HashSet<Int> = HashSet()
    lateinit var context: Context

    private var patients = emptyList<Patient>() // Cached copy of patients

    inner class PatientViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView)

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): PatientViewHolder {
        val itemView = LayoutInflater.from(parent.context).inflate(R.layout.recyclerview_item, parent, false)
        val vh = PatientViewHolder(itemView)
        context = parent.context
        return vh
    }

    @SuppressLint("SetTextI18n")
    override fun onBindViewHolder(holder: PatientViewHolder, position: Int) {
        val current = patients[position]
        val patientID = current.patientID.toString()
        val name = current.name
        val lastName = current.lastName
        val status = current.status
        val age = current.age
        val nationality = current.nationality
        val region = current.region
        val pathologies = current.pathologies
        val medication = current.medication
        val isHospitalized = current.isHospitalized
        val isInUCI = current.isInUCI

        // Add data to cells
        holder.itemView.title_textView.text = "$patientID - $lastName, $name\nStatus: $status"
        var expandedText = "Age: $age\nCountry: $nationality, $region\nPathologies: $pathologies\nMedication: $medication"
        if (isHospitalized) {
            holder.itemView.expanded_textView.text =
                "$expandedText\n* IS HOSPITALIZED *"
        } else if (isInUCI) {
            holder.itemView.expanded_textView.text =
                "$expandedText\n*IS IN UCI*"
        } else {
            holder.itemView.expanded_textView.text =
                expandedText
        }

        // Expand when you click on cell
        holder.itemView.expand_layout.setOnExpandListener(object :
            ExpandableLayout.OnExpandListener {
            override fun onExpand(expanded: Boolean) {
                if (expandedPositionSet.contains(position)) {
                    expandedPositionSet.remove(position)
                } else {
                    expandedPositionSet.add(position)
                }
            }
        })
        holder.itemView.expand_layout.setExpand(expandedPositionSet.contains(position))

        // Edit button
        val editButton = holder.itemView.editButton
        editButton.setOnClickListener {
            // Open new activity and send patient information to edit
            val intent = Intent(this.context, EditPatientActivity::class.java)
            val gson = Gson()
            val pString = gson.toJson(current)
            intent.putExtra("patient", pString)
            context.startActivity(intent)
        }
    }

    internal fun setPatients(patients: List<Patient>) {
        this.patients = patients
        notifyDataSetChanged()
    }

    override fun getItemCount() = patients.size
}