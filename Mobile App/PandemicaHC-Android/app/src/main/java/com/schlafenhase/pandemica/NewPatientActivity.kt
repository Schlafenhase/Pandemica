package com.schlafenhase.pandemica

import android.app.Activity
import android.content.Intent
import android.os.Bundle
import android.text.TextUtils
import android.view.View
import android.widget.Button
import android.widget.EditText
import android.widget.RadioButton
import android.widget.Switch
import androidx.appcompat.app.AppCompatActivity
import com.google.gson.Gson
import com.schlafenhase.pandemica.data.Patient

class NewPatientActivity : AppCompatActivity() {

    private lateinit var nameView: EditText
    private lateinit var lastNameView: EditText
    private lateinit var patientIDView: EditText
    private lateinit var ageView: EditText
    private lateinit var pathologiesView: EditText
    private lateinit var medicationView: EditText

    private lateinit var status: String
    private var isHospitalized: Boolean = false
    private var isInICU: Boolean = false

    public override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_new_patient)

        // Entry data recollection
        nameView = findViewById(R.id.name_entry)
        lastNameView = findViewById(R.id.last_name_entry)
        patientIDView = findViewById(R.id.patient_id_entry)
        ageView = findViewById(R.id.age_entry)
//        nationalityView = findViewById(R.id.edit_word)
//        regionView = findViewById(R.id.edit_word)
        pathologiesView = findViewById(R.id.pathologies_entry)
        medicationView = findViewById(R.id.medication_entry)

        val button = findViewById<Button>(R.id.button_save)
        button.setOnClickListener {
            val replyIntent = Intent()
            if (TextUtils.isEmpty(nameView.text) || TextUtils.isEmpty(lastNameView.text) || TextUtils.isEmpty(patientIDView.text)
                || TextUtils.isEmpty(ageView.text) || TextUtils.isEmpty(pathologiesView.text) || TextUtils.isEmpty(medicationView.text)) {
                setResult(Activity.RESULT_CANCELED, replyIntent)
            } else {
                val name = nameView.text.toString()
                val lastName = lastNameView.text.toString()
                val patientID = patientIDView.text.toString().toInt()
                val age = ageView.text.toString().toInt()
                val pathologies = pathologiesView.text.toString()
                val medication = medicationView.text.toString()

                val gson = Gson()
                val nPatient = Patient(name = name, lastName = lastName, patientID = patientID, age = age, nationality = "Costa Rica",
                                        region = "The Americas", pathologies = pathologies, status = status, medication = medication,
                                        isHospitalized = isHospitalized, isInUCI = isInICU)
                val nPatientString = gson.toJson(nPatient)

                replyIntent.putExtra(EXTRA_REPLY, nPatientString)
                setResult(Activity.RESULT_OK, replyIntent)
            }
            finish()
        }


        // Load switchers
        val iHSwitch = findViewById<Switch>(R.id.isHospitalized_switch)
        iHSwitch.setOnCheckedChangeListener { _, isChecked ->
            isHospitalized = isChecked
        }
        val ICUSwitch = findViewById<Switch>(R.id.isHospitalized_switch)
        ICUSwitch.setOnCheckedChangeListener { _, isChecked ->
            isInICU = isChecked
        }

        // Set action bar back button
        supportActionBar?.setDisplayHomeAsUpEnabled(true);
        supportActionBar?.title = resources.getString(R.string.add_new_patient)
    }

    companion object {
        const val EXTRA_REPLY = "com.schlafenhase.pandemica.patientlistsql.REPLY"
    }

    fun onRadioButtonClicked(view: View) {
        if (view is RadioButton) {
            // Is the button now checked?
            val checked = view.isChecked

            // Check which radio button was clicked
            when (view.getId()) {
                R.id.aliveRadio ->
                    if (checked) {
                        status = "Alive"
                    }
                R.id.infectedRadio ->
                    if (checked) {
                        status = "Infected"
                    }
                R.id.recoveredRadio ->
                    if (checked) {
                        status = "Recovered"
                    }
                R.id.deadRadio ->
                    if (checked) {
                        status = "Dead"
                    }
            }
        }
    }

    override fun onSupportNavigateUp(): Boolean {
        finish()
        return true
    }
}