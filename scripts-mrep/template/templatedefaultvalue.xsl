﻿<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:query('select * from str_documenttemplatedefaultvalue where languageid = 1')">
          <templatedefaultvalue>
            <defaultvalue m:left="-156" m:top="179">
              <xsl:value-of select="defaultvalue" />
            </defaultvalue>
            <templatedefaultvalueid m:left="-30" m:top="197">pk:templatedefaultvalue(<xsl:value-of select="macrosfield" />)</templatedefaultvalueid>
            <insertfield m:left="-27" m:top="161">
              <xsl:value-of select="macrosfield" />
            </insertfield>
          </templatedefaultvalue>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
</xsl:stylesheet>