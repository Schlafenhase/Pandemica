package com.schlafenhase.pandemica.ui.reports

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import androidx.appcompat.app.AppCompatActivity
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProviders
import com.schlafenhase.pandemica.R

class ReportsFragment : Fragment() {

    private lateinit var reportsViewModel: ReportsViewModel

    override fun onCreateView(
            inflater: LayoutInflater,
            container: ViewGroup?,
            savedInstanceState: Bundle?
    ): View? {
        val root = inflater.inflate(R.layout.fragment_reports, container, false)
        (activity as AppCompatActivity?)!!.supportActionBar!!.show()

        val patientsByStatusBtn = root.findViewById<View>(R.id.patientsByStatusButton) as Button
        val casesLasWeekBtn = root.findViewById<View>(R.id.casesLastWeekButton) as Button
        patientsByStatusBtn.setOnClickListener { generateReport("patientsByStatus") }
        patientsByStatusBtn.setOnClickListener { generateReport("casesLastWeek") }

        return root
    }

    fun generateReport(type: String) {
        if (type == "patientsByStatus") {
            // Generate report
        } else {
            // Generate report
        }
    }
}
