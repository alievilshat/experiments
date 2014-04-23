<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:terapy="http://www.navitas.nl/2014/Mapper/dbaccess/terapy" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <terapy>
        <xsl:for-each select="terapy:query('select distinct colour from _products')">
          <featurevalue>
            <featurevalueid m:left="-65" m:top="161">pk:featurevalue(<xsl:value-of select="colour" />)</featurevalueid>
            <featureid m:left="59" m:top="178">1</featureid>
            <optionvalue m:left="-66" m:top="193">
              <xsl:value-of select="colour" />
            </optionvalue>
          </featurevalue>
        </xsl:for-each>
      </terapy>
    </target>
  </xsl:template>
</xsl:stylesheet>